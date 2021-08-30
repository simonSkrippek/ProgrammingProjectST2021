using System;
using TMPro;
using UnityEngine;
using WhackAStoodent.Events;

namespace WhackAStoodent.UI.RespondToPlayRequestScreen
{
    public class RespondToPlayRequestScreenController : MonoBehaviour
    {
        [SerializeField] private StringEvent receivedPlayRequestEvent;
        [SerializeField] private TextMeshProUGUI senderText;

        private void OnEnable()
        {
            receivedPlayRequestEvent.Subscribe(UpdatePlayRequestSenderText);
        }
        private void OnDisable()
        {
            receivedPlayRequestEvent.Unsubscribe(UpdatePlayRequestSenderText);
        }

        private void UpdatePlayRequestSenderText(string senderUsername)
        {
            senderText.text = senderUsername;
        }
    }
}
