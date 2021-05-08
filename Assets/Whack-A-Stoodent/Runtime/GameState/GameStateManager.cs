using WhackAStoodent.Runtime.Helper;

namespace WhackAStoodent.Runtime.GameState
{
    public class GameStateManager : APersistantSingletonManagerScript<GameStateManager>
    {
        
        
        private enum EGameState
        {
            PreConnect,
            Connecting,
            MainMenu,
            WaitingForAnswerToPlayRequest,
            AnsweringPlayRequest,
            InGame,
            InResultsScreen,
        }

        //ConnectingState Data
        private EGameState? _stateBeforeConnecting = new EGameState();
        
        
    }
    
    
}