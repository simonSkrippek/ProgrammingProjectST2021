using TMPro;
using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;

namespace WhackAStoodent.UI.UserStatsUI
{
    public class MatchHistoryEntryDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moleUsernameText;
        [SerializeField] private TextMeshProUGUI moleScoreText;
        [SerializeField] private TextMeshProUGUI hitterUsernameText;
        [SerializeField] private TextMeshProUGUI hitterScoreText;
        
        public void Init(MatchHistoryEntry matchData)
        {
            if (matchData._playerGameRole == EGameRole.Mole)
            {
                moleUsernameText.text = $"{matchData._playerName.ToUpper()} (YOU)";
                moleScoreText.text = $"{matchData._playerScore}";
                
                hitterUsernameText.text = matchData._opponentName.ToUpper();
                hitterScoreText.text = $"{matchData._opponentScore}"; 
            }
            else
            {
                hitterUsernameText.text = $"{matchData._playerName.ToUpper()} (YOU)";
                hitterScoreText.text = $"{matchData._playerScore}";
                
                moleUsernameText.text = matchData._opponentName.ToUpper();
                moleScoreText.text = $"{matchData._opponentScore}";
            }
        }
    }
}