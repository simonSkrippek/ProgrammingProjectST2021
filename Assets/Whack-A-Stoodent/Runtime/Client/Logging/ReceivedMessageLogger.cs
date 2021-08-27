using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Events;

namespace WhackAStoodent.Client.Logging
{
    public class ReceivedMessageLogger : MonoBehaviour
    {
        [SerializeField] private NoParameterEvent readyForAuthenticationEvent;
        [SerializeField] private AMessageEvent messageReceivedEvent;
        [SerializeField] private NoParameterEvent connectionInterruptedEvent;
        private void Awake()
        {
            readyForAuthenticationEvent.Subscribe(OnReadyForAuthentication);
            messageReceivedEvent.Subscribe(OnMessageReceivedHandler);
            connectionInterruptedEvent.Subscribe(OnConnectionInterrupted);
        }
        private void OnDestroy()
        {
            readyForAuthenticationEvent.Unsubscribe(OnReadyForAuthentication);
            messageReceivedEvent.Unsubscribe(OnMessageReceivedHandler);
            connectionInterruptedEvent.Unsubscribe(OnConnectionInterrupted);
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