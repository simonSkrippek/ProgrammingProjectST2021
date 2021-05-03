using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using ENet;
using UnityEditor.VersionControl;
using UnityEngine;
using WhackAStoodent.Runtime.Networking.ENet;
using WhackAStoodent.Runtime.Networking.Messages;
using Event = ENet.Event;
using EventType = ENet.EventType;

namespace WhackAStoodent.Runtime.Networking.Connectors
{
    public class NetworkConnector : AConnector
    {
        public override event Action ConnectedToServer;
        public override event Action DisconnectedFromServer;
        public override event Action ServerConnectionTimedOut;
        public override event Action<AMessage> ReceivedMessageFromServer;
        
        public bool WasInitializedProperly { get;  }

        private Host _clientHost;
        private bool _connectedToServerPeer;
        private Peer _serverPeer;
        
        private readonly string _ipAddress;
        private readonly ushort _port;
        private readonly uint _timeoutTime;
        
        private Thread _connectorThread;
        private bool isConnectorThreadRunning = true;

        private readonly ConcurrentQueue<ReceivedConnectionAttemptMessage> _peerConnectionAttemptMessages = new ConcurrentQueue<ReceivedConnectionAttemptMessage>();
        private readonly ConcurrentQueue<ReceivedDisconnectionMessage> _peerDisconnectionMessages = new ConcurrentQueue<ReceivedDisconnectionMessage>();
        private readonly ConcurrentQueue<ReceivedTimeOutMessage> _peerTimeOutMessages = new ConcurrentQueue<ReceivedTimeOutMessage>();

        private readonly ConcurrentQueue<ReceivedCustomMessage> _peerReceiveMessages = new ConcurrentQueue<ReceivedCustomMessage>();

        private readonly ConcurrentQueue<CustomMessageToSend> _messagesToSend = new ConcurrentQueue<CustomMessageToSend>();

        private readonly List<Packet> _packetsToDispose = new List<Packet>();

        public NetworkConnector()
        {
            if (GetConnectionSettings(out NetworkConnectionSettings network_connection_settings))
            {
                _ipAddress = network_connection_settings.ipAddress;
                _port = network_connection_settings.port;
                _timeoutTime = network_connection_settings.timeoutTime;
            }
            else
            {
                Debug.Log($"something went wrong when attempting to find a settings asset of type {typeof(NetworkConnectionSettings)} when constructing a connector of type {typeof(NetworkConnector)}");
                return;
            }

            WasInitializedProperly = true;
        }

        public override bool Connect()
        {
            if (!EnetInitializer.Initialize())
            {
                return false;
            }

            _clientHost = null;
            try
            {
                _clientHost = new Host();
                _clientHost.Create(1, 0);

                Address address = new Address();
                address.SetHost(_ipAddress);
                address.Port = _port;
                
                _connectorThread = CreateConnectorThread();
                _connectorThread.Start();
                
                _serverPeer = _clientHost.Connect(address);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                Dispose();
                return false;
            }

            return true;
        }
        public override void Disconnect()
        {
            if (isConnectorThreadRunning)
            {
                _serverPeer.Disconnect((uint)EDisconnectionReason.GameFlow);
                isConnectorThreadRunning = false;
                _connectorThread.Join();
            }
        }
        public override void Dispose()
        {
            Disconnect();
            _clientHost?.Dispose();
            _clientHost = null;
            EnetInitializer.Deinitialize();
        }

        public override void SendMessage(byte[] message)
        {
            _messagesToSend.Enqueue(new CustomMessageToSend(_serverPeer, 0, message));
        }

        public override void SendMessage(AMessage message)
        {
            if (MessageParser.ParseMessage(message, out byte[] message_bytes))
                SendMessage(message_bytes);
            else
                Debug.LogError($"Message of type {message.GetType()} could not be parsed and thus not sent to the server.");
        }

        public override void RaiseEventsForReceivedMessages()
        {
            while (_peerConnectionAttemptMessages.TryDequeue(out ReceivedConnectionAttemptMessage connection_attempt_message))
            {
                if(HandlePeerConnectionAttempt(connection_attempt_message))
                    ConnectedToServer?.Invoke();
            }
            while (_peerDisconnectionMessages.TryDequeue(out ReceivedDisconnectionMessage disconnection_message))
            {
                if(HandlePeerDisconnection(disconnection_message))
                    DisconnectedFromServer?.Invoke();
            }
            while (_peerTimeOutMessages.TryDequeue(out ReceivedTimeOutMessage time_out_message))
            {
                if(HandlePeerTimeout(time_out_message))
                    ServerConnectionTimedOut?.Invoke();
            }

            while (_peerReceiveMessages.TryDequeue(out ReceivedCustomMessage received_message))
            {
                if (MessageParser.ParseMessage(received_message.Message, out AMessage parsed_message))
                    ReceivedMessageFromServer?.Invoke(parsed_message);
            }
        }

        private bool HandlePeerConnectionAttempt(ReceivedConnectionAttemptMessage connectionAttemptMessage)
        {
            if (IsServerPeer(connectionAttemptMessage.Sender))
            {
                _connectedToServerPeer = true;
                return true;
            }
            else
            {
                connectionAttemptMessage.Sender.Disconnect((uint)EDisconnectionReason.InvalidPeer);
                return false;
            }
        }
        private bool HandlePeerDisconnection(ReceivedDisconnectionMessage disconnectionMessage)
        {
            if(IsServerPeer(disconnectionMessage.Sender))
            {
                _serverPeer = default;
                _connectedToServerPeer = false;
                Debug.Log("the server has disconnected");
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool HandlePeerTimeout(ReceivedTimeOutMessage timeOutMessage)
        {
            if(IsServerPeer(timeOutMessage.Sender))
            {
                _serverPeer = default;
                _connectedToServerPeer = false;
                Debug.Log("the server has timed out");
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsConnected()
        {
            return _connectedToServerPeer && _serverPeer.State == PeerState.Connected;
        }
        private bool IsConnected(Peer peer)
        {
            return _connectedToServerPeer && IsServerPeer(peer) && _serverPeer.State == PeerState.Connected;
        }
        private bool IsServerPeer(Peer peer)
        {
            return _serverPeer.IsSet && _serverPeer.ID == peer.ID;
        }

        #region connector thread handling
        private Thread CreateConnectorThread()
        {
            return _connectorThread = new Thread(ConnectorThreadStart);
        }
        private void ConnectorThreadStart()
        {
            while (isConnectorThreadRunning)
            {
                HandleIncomingMessages();

                HandleOutgoingMessages();

                HandleDisposingUsedPackets();
            }
        }
        
        private void HandleIncomingMessages()
        {
            bool HasNetworkEvent(out Event networkEvent) => !(_clientHost.CheckEvents(out networkEvent) > 0 || _clientHost.Service((int) _timeoutTime, out networkEvent) > 0);

            if (HasNetworkEvent(out Event network_event))
            {
                HandleIncomingMessage(network_event);
            }
        }
        private void HandleIncomingMessage(Event networkEvent)
        {
            switch (networkEvent.Type)
            {
                case EventType.Connect:
                    _peerConnectionAttemptMessages.Enqueue(new ReceivedConnectionAttemptMessage(networkEvent.Peer));
                    break;
                case EventType.Disconnect:
                    _peerDisconnectionMessages.Enqueue(new ReceivedDisconnectionMessage(networkEvent.Peer));
                    break;
                case EventType.Receive:
                    Packet packet = networkEvent.Packet;
                    byte[] packet_data = new byte[packet.Length];
                    packet.CopyTo(packet_data);
                    _peerReceiveMessages.Enqueue(new ReceivedCustomMessage(networkEvent.Peer, packet_data));
                    break;
                case EventType.Timeout:
                    _peerTimeOutMessages.Enqueue(new ReceivedTimeOutMessage(networkEvent.Peer));
                    break;
                case EventType.None:
                    //Debug.Log("Received network event without type");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(networkEvent.Type),"Received network event with unknown type");
            }
        }

        private void HandleOutgoingMessages()
        {
            while (_messagesToSend.TryDequeue(out CustomMessageToSend message_to_send))
            {
                SendOutgoingMessage(message_to_send);
            }
        }

        private void SendOutgoingMessage(CustomMessageToSend messageToSend)
        {
            if (IsConnected(messageToSend.Recipient))
            {
                Packet packet = default;
                packet.Create(messageToSend.Message, PacketFlags.Reliable);
                if (messageToSend.Recipient.Send(0, ref packet))
                {
                    _packetsToDispose.Add(packet);
                }
                else
                {
                    Debug.Log("message could not be sent successfully");
                }
            }
        }

        private void HandleDisposingUsedPackets()
        {
            if (_packetsToDispose.Count > 0)
            {
                _clientHost.Flush();
                foreach (Packet packet in _packetsToDispose)
                {
                    packet.Dispose();
                }

                _packetsToDispose.Clear();
            }
        }
        #endregion
    }

    
}
