using System;
using WhackAStoodent.Runtime.Client;
using WhackAStoodent.Runtime.Helper;

namespace WhackAStoodent.Runtime.GameState
{
    public class GameStateManager : APersistantSingletonManagerScript<GameStateManager>
    {
        private ClientManager ClientManager => ClientManager.Instance;
        
        private EGameState _currentGameState = EGameState.PreConnect;
        
        private enum EGameState
        {
            PreConnect,
            Connecting,
            Unauthenticated,
            
            MainMenu,
            WaitingForAnswerToPlayRequest,
            AnsweringPlayRequest,
            InGame,
            InResultsScreen,
        }

        //ConnectingState Data
        private EGameState? _stateBeforeConnecting = null;

        private void SwitchGameState(EGameState gameState)
        {
            
        }

        public void HandleConfirmInput()
        {
            
        }

        public void HandleReturnInput()
        {
            switch (_currentGameState)
            {
                case EGameState.PreConnect:
                    break;
                case EGameState.Connecting:
                    break;
                case EGameState.MainMenu:
                    break;
                case EGameState.WaitingForAnswerToPlayRequest:
                    break;
                case EGameState.AnsweringPlayRequest:
                    break;
                case EGameState.InGame:
                    break;
                case EGameState.InResultsScreen:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Start()
        {
            
        }
    }
    
    
}