using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using WhackAStoodent.Client;
using WhackAStoodent.Helper;

namespace WhackAStoodent.GameState
{
    public class GameStateManager : ASingletonManagerScript<GameStateManager>
    {
        private ClientManager ClientManager => ClientManager.Instance;
        
        private EGameState _currentGameState = EGameState.PreConnect;

        private SceneManager SceneManager => WhackAStoodent.SceneManager.Instance;
        
        private enum EGameState
        {
            //not connected, game just started or was just reset. networking scene needs to be newly loaded
            PreConnect,
            //not connected, game just started or was just reset. networking scene needs to be newly loaded
            Connecting,
            //connected, but not authenticated -> no action, can only authenticate. back quits
            Unauthenticated,
            
            //connected, authenticated, doing menu stuff -> can request games or receive game requests. back bring up want to quit menu.
            Authenticated,
            //after sending a play request -> no action, can only wait for response. back does nothing?
            WaitingForAnswerToPlayRequest,
            //after receiving a play request -> action required, accept or deny. back denies
            AnsweringPlayRequest,
            //when loading, before playing -> may only send finished loading message, until receiving one. back does nothing
            LoadingPlaySession,
            //while playing -> may send gameplay messages and receive them, until game end messages
            InPlaySession,
            //same as authenticated -> may send gameplay messages and receive them, until game end messages
            InResultsScreen,
        }

        private void ChangeGameState(EGameState gameState)
        {
            if (gameState == EGameState.PreConnect)
            {
                if(SceneManager.IsSceneLoadedOrLoading(Scenes.Networking.Index()))
                {
                    ReloadNetworking();
                }
                else
                {
                    
                }
            }
            else if (gameState == EGameState.Connecting)
            {
            }
            else if (gameState == EGameState.Unauthenticated)
            {
            }
            else if (gameState == EGameState.Authenticated)
            {
            }
            else if (gameState == EGameState.WaitingForAnswerToPlayRequest)
            {
            }
            else if (gameState == EGameState.AnsweringPlayRequest)
            {
                
            }
            else if (gameState == EGameState.LoadingPlaySession)
            {
            }
            else if (gameState == EGameState.InPlaySession)
            {
            }
            else if (gameState == EGameState.InResultsScreen)
            {
                
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private void ReloadNetworking()
        {
            SceneManager.UnloadSceneAsync(Scenes.Networking.Index(), (_) =>
            {
                LoadNetworking();
            });
        }
        private void LoadNetworking()
        {
            SceneManager.LoadSceneAsync(Scenes.Networking.Index(), LoadSceneMode.Additive);
        }

        /// <summary>
        /// do sth when the confirm input is given, or the confirm button is pressed in whatever scene we are in
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void HandleConfirmInput()
        {
            switch (_currentGameState)
            {
                case EGameState.PreConnect:
                    break;
                case EGameState.Connecting:
                    break;
                case EGameState.Unauthenticated:
                    break;
                case EGameState.Authenticated:
                    break;
                case EGameState.WaitingForAnswerToPlayRequest:
                    break;
                case EGameState.AnsweringPlayRequest:
                    AcceptPlayRequest();
                    break;
                case EGameState.LoadingPlaySession:
                    break;
                case EGameState.InPlaySession:
                    break;
                case EGameState.InResultsScreen:
                    ChangeGameState(EGameState.Authenticated);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// do sth when the return input is given, or the return button is pressed in whatever scene we are in
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void HandleReturnInput()
        {
            switch (_currentGameState)
            {
                case EGameState.PreConnect:
                    SceneManager.QuitApplication();
                    break;
                case EGameState.Connecting:
                    SceneManager.QuitApplication();
                    break;
                case EGameState.Unauthenticated:
                    SceneManager.QuitApplication();
                    break;
                case EGameState.Authenticated:
                    //TODO trigger want to quit menu
                    SceneManager.QuitApplication();
                    break;
                case EGameState.WaitingForAnswerToPlayRequest:
                    //nothing? TODO should cancel play request
                    break;
                case EGameState.AnsweringPlayRequest:
                    DeclinePlayRequest();
                    break;
                case EGameState.LoadingPlaySession:
                    //nothing
                    break;
                case EGameState.InPlaySession:
                    //nothing?
                    break;
                case EGameState.InResultsScreen:
                    ChangeGameState(EGameState.Authenticated);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AcceptPlayRequest()
        {
            //Todo send message for accepting
            
        }
        private void DeclinePlayRequest()
        {
            //Todo send message for declining
            
            ChangeGameState(EGameState.Authenticated);
        }

        private void Start()
        {
            
        }
    }
    
    
}