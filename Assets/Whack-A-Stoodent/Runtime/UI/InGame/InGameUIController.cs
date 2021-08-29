using System;
using TMPro;
using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Events;
using WhackAStoodent.Helper;

namespace WhackAStoodent.UI.InGame
{
    public class InGameUIController : MonoBehaviour
    {
        [SerializeField] private StringGameRoleEvent gameStartedEvent;
        [SerializeField] private LongEvent hitterScoredEvent;
        [SerializeField] private LongEvent moleScoredEvent;
        
        [SerializeField] private TextMeshProUGUI moleScoreText;
        [SerializeField] private TextMeshProUGUI hitterScoreText;
        [SerializeField] private TextMeshProUGUI moleUsernameText;
        [SerializeField] private TextMeshProUGUI hitterUsernameText;

        private void Awake()
        {
            gameStartedEvent.Subscribe(HandleGameStarted);
            hitterScoredEvent.Subscribe(HandleHitterScored);
            moleScoredEvent.Subscribe(HandleMoleScored);
        }

        private void HandleGameStarted(string opponentUserName, EGameRole opponentGameRole)
        {
            string mole_username;
            string hitter_username;
            if (opponentGameRole == EGameRole.Hitter)
            {
                hitter_username = opponentUserName;
                mole_username = StorageUtility.LoadClientName();
            }
            else
            {
                hitter_username = StorageUtility.LoadClientName();
                mole_username = opponentUserName;
            }

            moleUsernameText.text = mole_username;
            hitterUsernameText.text = hitter_username;
        }

        private void HandleMoleScored(long newScore)
        {
            moleScoreText.text = newScore.ToString();
        }
        private void HandleHitterScored(long newScore)
        {
            hitterScoreText.text = newScore.ToString();
        }
    }
}
