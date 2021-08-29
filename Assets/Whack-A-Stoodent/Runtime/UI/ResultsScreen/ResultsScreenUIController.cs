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
            //TODO fix once gamerRoles are part of the matchData object
            var opponent_game_role = EGameRole.Hitter;
            if (opponent_game_role == EGameRole.Hitter)
            {
                hitterUsernameText.text = matchData._opponentName;
                hitterScoreText.text = matchData._opponentScore.ToString();
                moleUsernameText.text = matchData._playerName;
                moleScoreText.text = matchData._playerScore.ToString();
            }
            else
            {
                hitterUsernameText.text = matchData._playerName;
                hitterScoreText.text = matchData._playerScore.ToString();
                moleUsernameText.text = matchData._opponentName;
                moleScoreText.text = matchData._opponentScore.ToString();
            }

            titleText.text = matchData._opponentScore == matchData._playerScore? "TIE!" : (matchData._opponentScore > matchData._playerScore ? matchData._opponentName.ToUpper() : "YOU") + "\nWON!";
        }
    }
}