using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Events;

namespace WhackAStoodent.Client.Logging
{
    public class ReceivedMessageLogger : MonoBehaviour
    {
        [SerializeField] private NoParameterEvent readyForAuthenticationEvent;
        private void Awake()
        {
            readyForAuthenticationEvent.Subscribe(OnReadyForAuthentication);
            ClientManager.Instance.MessageReceived += OnMessageReceivedHandler;
            ClientManager.Instance.ConnectionInterrupted += OnConnectionInterrupted;
        }
        private void OnDestroy()
        {
            readyForAuthenticationEvent.Unsubscribe(OnReadyForAuthentication);
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
    }
}