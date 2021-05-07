using UnityEngine;
using WhackAStoodent.Runtime.Client.Networking.Messages;

namespace WhackAStoodent.Runtime.Client.Logging
{
    public class SentMessageLogger : MonoBehaviour
    {
        private void Awake()
        {
            ClientManager.Instance.RequestedSendingMessageToServer += OnRequestedSendingMessageToServer;
            ClientManager.Instance.SentMessageToServer += OnSentMessageToServer;
        }
        private void OnRequestedSendingMessageToServer(AMessage obj)
        {
            if (enabled)
            {
                Debug.Log($"Client has requested to send a message of type {obj.MessageType} to the server");
            }
        }
        private void OnSentMessageToServer(AMessage obj)
        {
            if (enabled)
            {
                Debug.Log($"Client has successfully sent a message of type {obj.MessageType} to the server");
            }
        }

        private void OnDestroy()
        {
            ClientManager.Instance.RequestedSendingMessageToServer -= OnRequestedSendingMessageToServer;
            ClientManager.Instance.SentMessageToServer -= OnSentMessageToServer;
        }
    }
}