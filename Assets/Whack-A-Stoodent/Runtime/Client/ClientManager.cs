using System;
using JetBrains.Annotations;
using UnityEngine;
using WhackAStoodent.Client.Logging;
using WhackAStoodent.Client.Networking.Connectors;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Helper;

namespace WhackAStoodent.Client
{
    [RequireComponent(typeof(ReceivedMessageLogger), typeof(SentMessageLogger))]
    public class ClientManager : ASingletonManagerScript<ClientManager>
    {
        [SerializeField] private EConnectionType connectionType = default;
        private AConnector _connector;

        public static event Action<ClientManager> OnClientManagerAvailable; 
        
        public event Action ReadyForAuthentication;
        public event Action ConnectionInterrupted;
        public event Action<AMessage> MessageReceived;
        public event Action<AMessage> RequestedSendingMessageToServer;
        public event Action<AMessage> SentMessageToServer;
        
        public event Action<Guid, string> AuthenticationAcknowledged;
        public event Action AuthenticationDenied; 
        public event Action<byte[]> ReceivedPingResponse; 
        public event Action<MatchData[]> ReceivedMatchHistory; 
        public event Action AllPlayersLoadedGame; 
        public event Action<MatchData> GameEnded;
        public event Action<EHoleIndex> MoleLooked;
        public event Action MoleHid;
        public event Action<string, EGameRole> ReceivedPlayRequest; 
        public event Action<Vector2, EHoleIndex> HitterHitSuccessful; 
        public event Action<long> HitterScored;
        public event Action<Vector2> HitterHitFailed; 
        public event Action<long> MoleScored;
        public event Action<string, EGameRole> GameStarted;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (Instance == this)
            {
                OnClientManagerAvailable?.Invoke(this);
                
                InitializeConnector();
                if (!_connector.Connect())
                {
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
            ReadyForAuthentication?.Invoke();
        }
        private void OnDisconnectedFromServer()
        {
            ConnectionInterrupted?.Invoke();
        }
        private void OnServerConnectionTimedOut()
        {
            ConnectionInterrupted?.Invoke();
        }
        private void OnReceivedMessageFromServer(AMessage message)
        {
            MessageReceived?.Invoke(message);
            InvokeAppropriateMessageEvent(message);
        }
        private void OnRequestedSendingMessageToServer(AMessage message)
        {
            RequestedSendingMessageToServer?.Invoke(message);
        }
        private void OnSentMessageToServer(AMessage message)
        {
            SentMessageToServer?.Invoke(message);
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
                        AuthenticationAcknowledged?.Invoke(acknowledge_authentication_message._userID, acknowledge_authentication_message._userName);
                    }
                    break;
                case EMessageType.DenyAuthentication:
                    if(message is DenyAuthenticationMessage)
                    {
                        AuthenticationDenied?.Invoke();
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
                        ReceivedPingResponse?.Invoke(pong_message._pingData);
                    }
                    break;
                case EMessageType.MatchHistory:
                    if(message is MatchHistoryMessage match_history_message)
                    {
                        ReceivedMatchHistory?.Invoke(match_history_message._matchData);
                    }
                    break;
                case EMessageType.PlayRequest:
                    if(message is PlayRequestMessage play_request_message)
                    {
                        ReceivedPlayRequest?.Invoke(play_request_message._opponentName, play_request_message._playerGameRole);
                    }
                    break;
                case EMessageType.StartedGame:
                    if(message is StartedGameMessage started_game_message)
                    {
                        GameStarted?.Invoke(started_game_message._opponentName, started_game_message._playerGameRole);
                    }
                    break;
                case EMessageType.LoadedGame:
                    if(message is LoadedGameMessage)
                    {
                        AllPlayersLoadedGame?.Invoke();
                    }
                    break;
                case EMessageType.GameEnded:
                    if(message is GameEndedMessage game_ended_message)
                    {
                        GameEnded?.Invoke(game_ended_message._matchData);
                    }
                    break;
                case EMessageType.Look:
                    if(message is LookMessage look_message)
                    {
                        MoleLooked?.Invoke(look_message._holeIndex);
                    }
                    break;
                case EMessageType.Hide:
                    if(message is HideMessage)
                    {
                        MoleHid?.Invoke();
                    }
                    break;
                case EMessageType.HitSuccess:
                    if(message is HitSuccessMessage hit_success_message)
                    {
                        HitterHitSuccessful?.Invoke(hit_success_message._hitPosition, hit_success_message._holeIndex);
                        HitterScored?.Invoke(hit_success_message._pointsGained);
                    }
                    break;
                case EMessageType.HitFail:
                    if(message is HitFailMessage hit_fail_message)
                    {
                        HitterHitFailed?.Invoke(hit_fail_message._hitPosition);
                    }
                    break;
                case EMessageType.MoleScored:
                    if(message is MoleScoredMessage mole_scored_message)
                    {
                        MoleScored?.Invoke(mole_scored_message._pointsGained);
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