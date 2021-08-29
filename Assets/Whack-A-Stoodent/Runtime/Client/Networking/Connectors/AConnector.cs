using System;
using JetBrains.Annotations;
using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;

namespace WhackAStoodent.Client.Networking.Connectors
{
    public abstract class AConnector
    {
        public abstract event Action ConnectedToServer;

        public abstract event Action DisconnectedFromServer;
        
        public abstract event Action ServerConnectionTimedOut;

        public abstract event Action<AMessage> ReceivedMessageFromServer;
        public event Action<AMessage> RequestedSendingMessageToServer;
        public abstract event Action<AMessage> SentMessageToServer;
      
        
        public abstract bool Connect();
        public abstract void Disconnect();
        public abstract void Dispose();
        public void SendMessage(AMessage message)
        {
            RequestedSendingMessageToServer?.Invoke(message);
            SendMessageInternal(message);
        }
        protected abstract void SendMessageInternal(AMessage message);
        public abstract void RaiseEventsForReceivedMessages();

        public virtual void SendAuthenticateMessage(Guid? userID = null, [CanBeNull] string userName = null) => SendAuthenticateMessage(new AuthenticateMessage(userID, userName));
        public virtual void SendAuthenticateMessage(AuthenticateMessage message) => SendMessage(message);
        
        public virtual void SendPingMessage() => SendPingMessage(new PingMessage());
        public virtual void SendPingMessage(PingMessage message) => SendMessage(message);
        
        public virtual void SendPongMessage(byte[] pingData) => SendPongMessage(new PongMessage(pingData));
        public virtual void SendPongMessage(PongMessage message) => SendMessage(message);

        public virtual void SendGetMatchHistoryMessage() => SendGetMatchHistoryMessage(new GetMatchHistoryMessage());
        public virtual void SendGetMatchHistoryMessage(GetMatchHistoryMessage message) => SendMessage(message);
        
        public virtual void SendPlayWithRandomMessage() => SendPlayWithRandomMessage(new PlayWithRandomMessage());
        public virtual void SendPlayWithRandomMessage(PlayWithRandomMessage message) => SendMessage(message);
        
        public virtual void SendPlayWithSessionIDMessage(string sessionID) => SendPlayWithSessionIDMessage(new PlayWithSessionIDMessage(sessionID));
        public virtual void SendPlayWithSessionIDMessage(PlayWithSessionIDMessage message) => SendMessage(message);
        
        public virtual void SendAcceptPlayRequestMessage() => SendAcceptPlayRequestMessage(new AcceptPlayRequestMessage());
        public virtual void SendAcceptPlayRequestMessage(AcceptPlayRequestMessage message) => SendMessage(message);
        
        public virtual void SendDenyPlayRequestMessage(DenyPlayRequestMessage.EDenialReason? denialReason) => SendDenyPlayRequestMessage(new DenyPlayRequestMessage(denialReason ?? DenyPlayRequestMessage.EDenialReason.PlayerChoice));
        public virtual void SendDenyPlayRequestMessage(DenyPlayRequestMessage message) => SendMessage(message);
        
        public virtual void SendLoadedGameMessage() => SendLoadedGameMessage(new LoadedGameMessage());
        public virtual void SendLoadedGameMessage(LoadedGameMessage message) => SendMessage(message);
        
        public virtual void SendHitMessage(Vector2 position) => SendHitMessage(new HitMessage(position));
        public virtual void SendHitMessage(HitMessage message) => SendMessage(message);
        
        public virtual void SendLookMessage(EHoleIndex holeIndex) => SendLookMessage(new LookMessage(holeIndex));
        public virtual void SendLookMessage(LookMessage message) => SendMessage(message);
        
        public virtual void SendHideMessage() => SendHideMessage(new HideMessage());
        public virtual void SendHideMessage(HideMessage message) => SendMessage(message);


        protected bool GetConnectionSettings<TConnectionSettingsType>(out TConnectionSettingsType connectionSettings) where TConnectionSettingsType : AConnectionSettings
        {
            Debug.Log($"{this.GetType()}: Connector is requesting ConnectionSettings of type {nameof(TConnectionSettingsType)}");
            connectionSettings = (TConnectionSettingsType) Resources.Load($"{typeof(TConnectionSettingsType).Name}", typeof(TConnectionSettingsType));
            if (connectionSettings == null)
            {
                Debug.Log($"No connection settings asset found: needs to be located in Resources folder at path {(typeof(TConnectionSettingsType).Name)}");
                connectionSettings = null;
                return false;
            }
            return true;
        }
    }
}