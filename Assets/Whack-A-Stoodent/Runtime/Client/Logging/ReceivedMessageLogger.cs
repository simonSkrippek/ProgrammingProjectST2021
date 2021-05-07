using UnityEngine;
using WhackAStoodent.Runtime.Client.Networking.Messages;

namespace WhackAStoodent.Runtime.Client.Logging
{
    public class ReceivedMessageLogger : MonoBehaviour
    {
        private void Awake()
        {
            ClientManager.Instance.ReadyForAuthentication += OnReadyForAuthentication;
            ClientManager.Instance.MessageReceived += OnMessageReceivedHandler;
            ClientManager.Instance.ConnectionInterrupted += OnConnectionInterrupted;
        }
        private void OnReadyForAuthentication()
        {
            if (enabled)
            {
                Debug.Log($"Client has connected to server and is ready for authentication");
            }
        }
        private void OnMessageReceivedHandler(AMessage message)
        {
            if (enabled)
            {
                Debug.Log($"Client received message of type {message.MessageType}");
            }
        }
        private void OnConnectionInterrupted()
        {
            if (enabled)
            {
                Debug.Log($"Connection of the client to the server has been interrupted");
            }
        }

        private void OnDestroy()
        {
            ClientManager.Instance.ReadyForAuthentication -= OnReadyForAuthentication;
            ClientManager.Instance.MessageReceived -= OnMessageReceivedHandler;
            ClientManager.Instance.ConnectionInterrupted -= OnConnectionInterrupted;
        }
    }
}