using TMPro;
using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Events;

namespace WhackAStoodent.UI.ResultsScreen
{
    public class ResultsScreenUIController : MonoBehaviour
    {
        [SerializeField] private MatchDataEvent gameEndedEvent;
        
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI moleScoreText;
        [SerializeField] private TextMeshProUGUI hitterScoreText;
        [SerializeField] private TextMeshProUGUI moleUsernameText;
        [SerializeField] private TextMeshProUGUI hitterUsernameText;

        private void Awake()
        {
            gameEndedEvent.Subscribe(HandleGameEnded);
        }

        private void HandleGameEnded(MatchData matchData)
        {
            if (matchData._opponentGameRole == EGameRole.Hitter)
            {
                hitterUsernameText.text = matchData._opponentName.ToUpper();
                hitterScoreText.text = matchData._opponentScore.ToString();
                moleUsernameText.text = matchData._playerName.ToUpper();
                moleScoreText.text = matchData._playerScore.ToString();
            }
            else
            {
                hitterUsernameText.text = matchData._playerName.ToUpper();
                hitterScoreText.text = matchData._playerScore.ToString();
                moleUsernameText.text = matchData._opponentName.ToUpper();
                moleScoreText.text = matchData._opponentScore.ToString();
            }

            titleText.text = matchData._opponentScore == matchData._playerScore? "TIE!" : (matchData._opponentScore > matchData._playerScore ? matchData._opponentName.ToUpper() : "YOU") + "\nWON!";
        }
    }
}