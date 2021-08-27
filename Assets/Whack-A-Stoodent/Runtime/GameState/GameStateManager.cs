using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using WhackAStoodent.Client;
using WhackAStoodent.Events;
using WhackAStoodent.Helper;
using WhackAStoodent.UI;

namespace WhackAStoodent.GameState
{
    public class GameStateManager : ASingletonManagerScript<GameStateManager>
    {
        private ClientManager ClientManager => ClientManager.Instance;
        
        private EGameState _currentGameState = EGameState.PreConnect;

        private SceneManager SceneManager => WhackAStoodent.SceneManager.Instance;
        
        [Header("Listened Events")]
        [Header("User Input Events")]
        [SerializeField] private StringEvent usernameInputEvent;
        [SerializeField] private NoParameterEvent confirmInputEvent;
        [SerializeField] private NoParameterEvent returnInputEvent;
        [SerializeField] private HoleIndexEvent moleLookedInputEvent;
        [SerializeField] private NoParameterEvent moleHidInputEvent;
        [SerializeField] private Vector2Event hitterHitInputEvent;
        
        [Header("Server Response Events")]
        [SerializeField] private NoParameterEvent readyForAuthenticationEvent;
        [SerializeField] private StringEvent authenticationAcknowledgedEvent;
        [SerializeField] private NoParameterEvent authenticationDeniedEvent; 
        [SerializeField] private StringGameRoleEvent receivedPlayRequestEvent;
        [SerializeField] private NoParameterEvent allPlayersLoadedGameEvent;
        [SerializeField] private StringGameRoleEvent gameStartedEvent;
        [SerializeField] private MatchDataEvent gameEndedEvent;
        
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
            Debug.Log($"Game State Changed: {gameState}");
            switch (gameState)
            {
                case EGameState.PreConnect when SceneManager.IsSceneLoadedOrLoading(Scenes.Networking.Index()):
                    ReloadNetworking();
                    break;
                case EGameState.PreConnect:
                    LoadNetworking();
                    break;
                case EGameState.Connecting:
                    if(!SceneManager.IsSceneLoadedOrLoading(Scenes.UI.Index()))
                        SceneManager.LoadSceneAsync(Scenes.UI.Index(),
                            operation =>
                            {
                                operation.allowSceneActivation = true;
                                UIManager.Instance.ActivateUIScreen(UIManager.UIState.Connecting);
                            },
                            LoadSceneMode.Additive);
                    else
                        UIManager.Instance.ActivateUIScreen(UIManager.UIState.Connecting);
                    break;
                case EGameState.Unauthenticated:
                    UIManager.Instance.ActivateUIScreen(UIManager.UIState.UsernameInput);
                    break;
                case EGameState.Authenticated:
                    break;
                case EGameState.WaitingForAnswerToPlayRequest:
                    break;
                case EGameState.AnsweringPlayRequest:
                    break;
                case EGameState.LoadingPlaySession:
                    break;
                case EGameState.InPlaySession:
                    break;
                case EGameState.InResultsScreen:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
        
        public void ReloadNetworking()
        {
            SceneManager.UnloadSceneAsync(Scenes.InGame.Index());
            
            SceneManager.UnloadSceneAsync(Scenes.Networking.Index(), (_) =>
            {
                LoadNetworking();
            });
        }
        private void LoadNetworking()
        {
            SceneManager.LoadSceneAsync(
                Scenes.Networking.Index(),
                operation =>
                {
                    operation.allowSceneActivation = true;
                    ChangeGameState(EGameState.Connecting);

                },
                LoadSceneMode.Additive);
        }

        private void Start()
        {
            ChangeGameState(EGameState.PreConnect);
        }
    }
    
    
}