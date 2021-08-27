using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Events;

namespace WhackAStoodent.Client.Logging
{
    public class SentMessageLogger : MonoBehaviour
    {
        [SerializeField] private AMessageEvent requestedSendingMessageToServerEvent;
        [SerializeField] private AMessageEvent sentMessageToServerEvent;
        
        private void Awake()
        {
            requestedSendingMessageToServerEvent.Unsubscribe(OnRequestedSendingMessageToServer);
            sentMessageToServerEvent.Subscribe(OnSentMessageToServer);
        }
        private void OnDestroy()
        {
            requestedSendingMessageToServerEvent.Unsubscribe(OnRequestedSendingMessageToServer);
            sentMessageToServerEvent.Unsubscribe(OnSentMessageToServer);
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
    }
}