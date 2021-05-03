using System;
using ENet;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using WhackAStoodent.Runtime.Networking.Messages;

namespace WhackAStoodent.Runtime.Networking.Connectors
{
    public abstract class AConnector
    {
        public abstract event Action ConnectedToServer;

        public abstract event Action DisconnectedFromServer;
        
        public abstract event Action ServerConnectionTimedOut;

        public abstract event Action<AMessage> ReceivedMessageFromServer;
      
        
        public abstract bool Connect();
        public abstract void Disconnect();
        public abstract void Dispose();
        public abstract void SendMessage(byte[] message);
        public abstract void SendMessage(AMessage message);
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
        
        public virtual void SendAcceptPlayRequestMessage(string sessionCode) => SendAcceptPlayRequestMessage(new AcceptPlayRequestMessage(sessionCode));
        public virtual void SendAcceptPlayRequestMessage(AcceptPlayRequestMessage message) => SendMessage(message);
        
        public virtual void SendDenyPlayRequestMessage(string sessionCode, DenyPlayRequestMessage.EDenialReason? denialReason) => SendDenyPlayRequestMessage(new DenyPlayRequestMessage(sessionCode, denialReason ?? DenyPlayRequestMessage.EDenialReason.PlayerChoice));
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
            var found_asset_guids = AssetDatabase.FindAssets($"t:{nameof(TConnectionSettingsType)}");
            if (found_asset_guids == null || found_asset_guids.Length == 0)
            {
                connectionSettings = null;
                return false;
            }
            if (found_asset_guids.Length > 1)
            {
                Debug.Log($"Found more than one asset with matching type, will select the first one.");
            }
            var first_found_asset_guid = found_asset_guids[0];
            var first_found_asset_path = AssetDatabase.GUIDToAssetPath(first_found_asset_guid);
            if (string.IsNullOrEmpty(first_found_asset_path))
            {
                Debug.Log("Found Asset Path is invalid.");
                connectionSettings = null;
                return false;
            }
            connectionSettings = AssetDatabase.LoadAssetAtPath<TConnectionSettingsType>(first_found_asset_path);
            return connectionSettings;
        }
    }
}