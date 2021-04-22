using System;
using ENet;
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
        public abstract void RaiseEventsForReleasedMessages();
        
        
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