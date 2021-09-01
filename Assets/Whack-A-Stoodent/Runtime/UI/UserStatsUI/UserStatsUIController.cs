using System;
using TMPro;
using UnityEngine;
using WhackAStoodent.Client;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Events;

namespace WhackAStoodent.UI.UserStatsUI
{
    public class UserStatsUIController : MonoBehaviour
    {
        [Header("Subscribed Events")]
        [SerializeField] private MatchHistoryEntryArrayEvent receivedMatchHistory;
        [SerializeField] private UserStatsEvent receivedUserStats;
        
        [Header("MatchHistory References")]
        [SerializeField] private GameObject matchHistoryUIPanel;
        [SerializeField] private Transform matchHistoryUIContainer;
        [SerializeField] private MatchHistoryEntryDisplay matchHistoryEntryPrefab;
        
        [Header("UserStats References")]
        [SerializeField] private GameObject userStatsUIPanel;
        [SerializeField] private TextMeshProUGUI userStats_totalGamesPlayedText;
        [SerializeField] private TextMeshProUGUI userStats_gamesWonText;
        [SerializeField] private TextMeshProUGUI userStats_gamesLostText;
        [SerializeField] private TextMeshProUGUI userStats_lastGameEndedText;
        
        private void OnEnable()
        {
            receivedMatchHistory.Subscribe(HandleReceivedMatchHistory);
            receivedUserStats.Subscribe(HandleReceivedUserStats);
            
            ClientManager.Instance.RequestMatchHistory();
            ClientManager.Instance.RequestUserStats();
        }
        private void OnDisable()
        {
            receivedMatchHistory.Unsubscribe(HandleReceivedMatchHistory);
            receivedUserStats.Unsubscribe(HandleReceivedUserStats);
        }

        private void HandleReceivedUserStats(UserStats userStats)
        {
            userStats_totalGamesPlayedText.text = userStats._totalGamesPlayed.ToString();
            userStats_gamesWonText.text = userStats._gamesWon.ToString();
            userStats_gamesLostText.text = userStats._gamesLost.ToString();
            userStats_lastGameEndedText.text = userStats._lastOnline.ToShortDateString() + "\n" + userStats._lastOnline.ToShortTimeString();
        }

        private void HandleReceivedMatchHistory(MatchHistoryEntry[] matchHistoryEntries)
        {
            foreach (Transform child in matchHistoryUIContainer)
            {
                Destroy(child.gameObject);
            }
            foreach (var match_history_entry in matchHistoryEntries)
            {
                var new_display = Instantiate(matchHistoryEntryPrefab, matchHistoryUIContainer);
                new_display.Init(match_history_entry); 
            }
        }

        public void SwitchToMatchHistory()
        {
            matchHistoryUIPanel.SetActive(true);
            userStatsUIPanel.SetActive(false);
        }
        public void SwitchToUserStats()
        {
            matchHistoryUIPanel.SetActive(false);
            userStatsUIPanel.SetActive(true);
        }
    }
}
