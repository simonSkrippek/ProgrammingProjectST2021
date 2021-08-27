using System;
using JetBrains.Annotations;
using UnityEngine;
using WhackAStoodent.Client.Logging;
using WhackAStoodent.Client.Networking.Connectors;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Events;
using WhackAStoodent.Helper;

namespace WhackAStoodent.Client
{
    [RequireComponent(typeof(ReceivedMessageLogger), typeof(SentMessageLogger))]
    public class ClientManager : ASingletonManagerScript<ClientManager>
    {
        [SerializeField] private EConnectionType connectionType = default;
        private AConnector _connector;

        public static event Action<ClientManager> OnClientManagerAvailable; 
        
        [Header("Events")]
        [SerializeField] private NoParameterEvent readyForAuthentication;
        [SerializeField] private NoParameterEvent connectionInterrupted;
        [SerializeField] private AMessageEvent messageReceived;
        [SerializeField] private AMessageEvent requestedSendingMessageToServer;
        [SerializeField] private AMessageEvent sentMessageToServer;
        
        [SerializeField] private StringEvent authenticationAcknowledged;
        [SerializeField] private NoParameterEvent authenticationDenied; 
        [SerializeField] private ByteArrayEvent receivedPingResponse; 
        [SerializeField] private MatchDataArrayEvent receivedMatchHistory; 
        [SerializeField] private NoParameterEvent allPlayersLoadedGame; 
        [SerializeField] private StringGameRoleEvent receivedPlayRequest; 
        [SerializeField] private StringGameRoleEvent gameStarted;
        [SerializeField] private MatchDataEvent gameEnded;
        [SerializeField] private HoleIndexEvent moleLooked;
        [SerializeField] private NoParameterEvent moleHid;
        [SerializeField] private LongEvent moleScored;
        [SerializeField] private Vector2HoleIndexEvent hitterHitSuccessful; 
        [SerializeField] private Vector2Event hitterHitFailed; 
        [SerializeField] private LongEvent hitterScored;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (Instance == this)
            {
                OnClientManagerAvailable?.Invoke(this);
                
                InitializeConnector();
                if (!_connector.Connect())
                {
                    _connector.DisconnectedFromServer += OnDisconnectedFromServer;
                    Debug.Log("client host does not seem able to connect");
                }
            }

            Application.wantsToQuit += HandleApplicationQuit;
        }
        private void InitializeConnector()
        {
            _connector?.Dispose();
            
            _connector = connectionType switch
            {
                EConnectionType.Network => new NetworkConnector(),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _connector.ConnectedToServer += OnConnectedToServer;
            _connector.DisconnectedFromServer += OnDisconnectedFromServer;
            _connector.ServerConnectionTimedOut += OnServerConnectionTimedOut;
            _connector.ReceivedMessageFromServer += OnReceivedMessageFromServer;
            _connector.RequestedSendingMessageToServer += OnRequestedSendingMessageToServer;
            _connector.SentMessageToServer += OnSentMessageToServer;
        }
        private void OnConnectedToServer()
        {
            readyForAuthentication.Invoke();
        }
        private void OnDisconnectedFromServer()
        {
            connectionInterrupted.Invoke();
        }
        private void OnServerConnectionTimedOut()
        {
            connectionInterrupted.Invoke();
        }
        private void OnReceivedMessageFromServer(AMessage message)
        {
            messageReceived.Invoke(message);
            InvokeAppropriateMessageEvent(message);
        }
        private void OnRequestedSendingMessageToServer(AMessage message)
        {
            requestedSendingMessageToServer.Invoke(message);
        }
        private void OnSentMessageToServer(AMessage message)
        {
            sentMessageToServer.Invoke(message);
        }
        private bool HandleApplicationQuit()
        {
            _connector?.Dispose();
            _connector = null;
            return true;
        }
        
        
        protected override void OnDisable()
        {
            _connector?.Dispose();
            _connector = null;
        }
        
        private void InvokeAppropriateMessageEvent(AMessage message)
        {
            switch (message.MessageType)
            {
                case EMessageType.AcknowledgeAuthentication:
                    if(message is AcknowledgeAuthenticationMessage acknowledge_authentication_message)
                    {
                        StorageUtility.UpdateClientGuid(acknowledge_authentication_message._userID);
                        authenticationAcknowledged.Invoke(acknowledge_authentication_message._userName);
                    }
                    break;
                case EMessageType.DenyAuthentication:
                    if(message is DenyAuthenticationMessage)
                    {
                        authenticationDenied.Invoke();
                    }
                    break;
                case EMessageType.Ping:
                    if(message is PingMessage ping_message)
                    {
                        RespondToPing(ping_message._pingData);
                    }
                    break;
                case EMessageType.Pong:
                    if(message is PongMessage pong_message)
                    {
                        receivedPingResponse.Invoke(pong_message._pingData);
                    }
                    break;
                case EMessageType.MatchHistory:
                    if(message is MatchHistoryMessage match_history_message)
                    {
                        receivedMatchHistory.Invoke(match_history_message._matchData);
                    }
                    break;
                case EMessageType.PlayRequest:
                    if(message is PlayRequestMessage play_request_message)
                    {
                        receivedPlayRequest.Invoke(play_request_message._opponentName, play_request_message._playerGameRole);
                    }
                    break;
                case EMessageType.StartedGame:
                    if(message is StartedGameMessage started_game_message)
                    {
                        gameStarted.Invoke(started_game_message._opponentName, started_game_message._playerGameRole);
                    }
                    break;
                case EMessageType.LoadedGame:
                    if(message is LoadedGameMessage)
                    {
                        allPlayersLoadedGame.Invoke();
                    }
                    break;
                case EMessageType.GameEnded:
                    if(message is GameEndedMessage game_ended_message)
                    {
                        gameEnded.Invoke(game_ended_message._matchData);
                    }
                    break;
                case EMessageType.Look:
                    if(message is LookMessage look_message)
                    {
                        moleLooked.Invoke(look_message._holeIndex);
                    }
                    break;
                case EMessageType.Hide:
                    if(message is HideMessage)
                    {
                        moleHid.Invoke();
                    }
                    break;
                case EMessageType.HitSuccess:
                    if(message is HitSuccessMessage hit_success_message)
                    {
                        hitterHitSuccessful.Invoke(hit_success_message._hitPosition, hit_success_message._holeIndex);
                        hitterScored.Invoke(hit_success_message._pointsGained);
                    }
                    break;
                case EMessageType.HitFail:
                    if(message is HitFailMessage hit_fail_message)
                    {
                        hitterHitFailed.Invoke(hit_fail_message._hitPosition);
                    }
                    break;
                case EMessageType.MoleScored:
                    if(message is MoleScoredMessage mole_scored_message)
                    {
                        moleScored.Invoke(mole_scored_message._pointsGained);
                    }
                    break;
                default:
                    Debug.LogError($"{name}: received message of type {message.MessageType}. messages of that type should not be received?");
                    break;
            }
        }

        public void Authenticate([CanBeNull] string userName)
        {
            _connector.SendAuthenticateMessage(StorageUtility.LoadClientGuid(), userName);
        }
        public byte[] Ping()
        {
            PingMessage message = new PingMessage();
            _connector.SendPingMessage(message);
            return message._pingData;
        }
        private void RespondToPing(byte[] pingData)
        {
            _connector.SendPongMessage(pingData);
        }
        public void RequestMatchHistory()
        {
            _connector.SendGetMatchHistoryMessage();
        }
        public void RequestPlayWithRandom()
        {
            _connector.SendPlayWithRandomMessage();
        }
        public void RequestPlayWithSessionCode(string sessionCode)
        {
            _connector.SendPlayWithSessionIDMessage(sessionCode);
        }
        public void AcceptPlayRequest(string sessionCode)
        {
            _connector.SendAcceptPlayRequestMessage(sessionCode);
        }
        public void DenyPlayRequest(string sessionCode, DenyPlayRequestMessage.EDenialReason? reason = null)
        {
            _connector.SendDenyPlayRequestMessage(sessionCode, reason);
        }
        public void ConfirmGameIsLoaded()
        {
            _connector.SendLoadedGameMessage();
        }
        public void SendHitterHit(Vector2 position)
        {
            _connector.SendHitMessage(position);
        }
        public void SendMoleLook(EHoleIndex holeIndex)
        {
            _connector.SendLookMessage(holeIndex);
        }
        public void SendMoleHide()
        {
            _connector.SendHideMessage();
        }

        public string LoadUserName() => StorageUtility.LoadClientName();


        private void FixedUpdate()
        {
            _connector.RaiseEventsForReceivedMessages();
        }
    }

    public enum EConnectionType
    {
        Network
    }
}