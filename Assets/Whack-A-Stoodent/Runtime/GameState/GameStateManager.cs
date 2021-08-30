using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using WhackAStoodent.Client;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Events;
using WhackAStoodent.Helper;
using WhackAStoodent.Input;
using WhackAStoodent.UI;

namespace WhackAStoodent.GameState
{
    public class GameStateManager : ASingletonManagerScript<GameStateManager>
    {
        private ClientManager ClientManager => ClientManager.Instance;
        
        private EGameState _currentGameState = EGameState.PreConnect;

        private SceneManager SceneManager => WhackAStoodent.SceneManager.Instance;
        
        [Header("User Input Events")]
        [Header("Listened Events")]
        [SerializeField] private StringEvent usernameInputEvent;
        [SerializeField] private NoParameterEvent confirmInputEvent;
        [SerializeField] private NoParameterEvent returnInputEvent;
        [SerializeField] private NoParameterEvent mainMenuStartCreatePlayRequestInputEvent;
        [SerializeField] private NoParameterEvent mainMenuAbortCreatePlayRequestInputEvent;
        [SerializeField] private NoParameterEvent requestPlayWithRandomInputEvent;
        [SerializeField] private StringEvent requestPlayWithSessionCodeInputEvent;
        [SerializeField] private NoParameterEvent resultsScreenPlayAgainInputEvent;
        [SerializeField] private NoParameterEvent resultsScreenReturnInputEvent;
        [SerializeField] private NoParameterEvent acceptPlayRequestInputEvent;
        [SerializeField] private NoParameterEvent declinePlayRequestInputEvent;
        [SerializeField] private NoParameterEvent quitInputEvent;
        
        [SerializeField] private HoleIndexEvent moleLookedInputEvent;
        [SerializeField] private NoParameterEvent moleHidInputEvent;
        [SerializeField] private Vector2Event hitterHitInputEvent;
        
        [Header("Server Response Events")]
        [SerializeField] private NoParameterEvent readyForAuthenticationEvent;
        [SerializeField] private StringEvent authenticationAcknowledgedEvent;
        [SerializeField] private NoParameterEvent authenticationDeniedEvent; 
        [SerializeField] private StringEvent receivedPlayRequestEvent;
        [SerializeField] private DenialReasonEvent receivedDenyPlayRequestEvent;
        [SerializeField] private NoParameterEvent allPlayersLoadedGameEvent;
        [SerializeField] private StringGameRoleEvent gameStartedEvent;
        [SerializeField] private MatchDataEvent gameEndedEvent;

        //=================== UnityEvents ============================
        private void Start()
        {
            ChangeGameState(EGameState.PreConnect);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            
            readyForAuthenticationEvent.Subscribe(HandleReadyForAuthentication);
            authenticationAcknowledgedEvent.Subscribe(HandleAuthenticationAcknowledged);
            authenticationDeniedEvent.Subscribe(HandleAuthenticationDenied);
            receivedPlayRequestEvent.Subscribe(HandleReceivedPlayRequest);
            receivedDenyPlayRequestEvent.Subscribe(HandleReceivedDenyPlayRequest);
            gameStartedEvent.Subscribe(HandleGameStarted);
            allPlayersLoadedGameEvent.Subscribe(HandleAllPlayersLoadedGame);
            gameEndedEvent.Subscribe(HandleGameEnded);
            
            moleLookedInputEvent.Subscribe(HandleMoleLookedInput);
            moleHidInputEvent.Subscribe(HandleMoleHidInput);
            hitterHitInputEvent.Subscribe(HandleHitterHitInput);

            confirmInputEvent.Subscribe(HandleConfirmInput);
            returnInputEvent.Subscribe(HandleReturnInput);
            usernameInputEvent.Subscribe(HandleUsernameInput);
            mainMenuStartCreatePlayRequestInputEvent.Subscribe(HandleCreatePlayRequestInput);
            mainMenuAbortCreatePlayRequestInputEvent.Subscribe(HandleAbortCreatePlayRequestInput);
            requestPlayWithRandomInputEvent.Subscribe(HandleRequestPlayWithRandomInput);
            requestPlayWithSessionCodeInputEvent.Subscribe(HandleRequestPlayWithSessionCodeInput);
            resultsScreenPlayAgainInputEvent.Subscribe(HandlePlayAgainInput);
            resultsScreenReturnInputEvent.Subscribe(HandleReturnToMainMenuInput);
            acceptPlayRequestInputEvent.Subscribe(HandleAcceptPlayRequestInput);
            declinePlayRequestInputEvent.Subscribe(HandleDeclinePlayRequestInput);
            quitInputEvent.Subscribe(HandleQuitInput);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            
            readyForAuthenticationEvent.Unsubscribe(HandleReadyForAuthentication);
            authenticationAcknowledgedEvent.Unsubscribe(HandleAuthenticationAcknowledged);
            authenticationDeniedEvent.Unsubscribe(HandleAuthenticationDenied);
            receivedPlayRequestEvent.Unsubscribe(HandleReceivedPlayRequest);
            receivedDenyPlayRequestEvent.Unsubscribe(HandleReceivedDenyPlayRequest);
            gameStartedEvent.Unsubscribe(HandleGameStarted);
            allPlayersLoadedGameEvent.Unsubscribe(HandleAllPlayersLoadedGame);
            gameEndedEvent.Unsubscribe(HandleGameEnded);
            
            moleLookedInputEvent.Unsubscribe(HandleMoleLookedInput);
            moleHidInputEvent.Unsubscribe(HandleMoleHidInput);
            hitterHitInputEvent.Unsubscribe(HandleHitterHitInput);

            confirmInputEvent.Unsubscribe(HandleConfirmInput);
            returnInputEvent.Unsubscribe(HandleReturnInput);
            usernameInputEvent.Unsubscribe(HandleUsernameInput);
            mainMenuStartCreatePlayRequestInputEvent.Unsubscribe(HandleCreatePlayRequestInput);
            mainMenuAbortCreatePlayRequestInputEvent.Unsubscribe(HandleAbortCreatePlayRequestInput);
            requestPlayWithRandomInputEvent.Unsubscribe(HandleRequestPlayWithRandomInput);
            requestPlayWithSessionCodeInputEvent.Unsubscribe(HandleRequestPlayWithSessionCodeInput);
            resultsScreenPlayAgainInputEvent.Unsubscribe(HandlePlayAgainInput);
            resultsScreenReturnInputEvent.Unsubscribe(HandleReturnToMainMenuInput);
            acceptPlayRequestInputEvent.Unsubscribe(HandleAcceptPlayRequestInput);
            declinePlayRequestInputEvent.Unsubscribe(HandleDeclinePlayRequestInput);
            quitInputEvent.Unsubscribe(HandleQuitInput);
        }


        

        //================== Server Message Handling ===============
        private void HandleReadyForAuthentication()
        {
            Debug.Log("Ready for authentication");
            if (_currentGameState == EGameState.Connecting)
            {
                ChangeGameState(EGameState.Unauthenticated);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving ReadyForAuthentication in game state {_currentGameState}");  
                
            }
        }
        private void HandleAuthenticationAcknowledged(string username)
        {
            Debug.Log("Authentication Acknowledged received"); 
            if (_currentGameState == EGameState.Unauthenticated)
            {
                ChangeGameState(EGameState.Authenticated);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving authenticationAcknowledged in game state {_currentGameState}");  
                
            }
        }
        private void HandleAuthenticationDenied()
        {
            if (_currentGameState == EGameState.Unauthenticated)
            {
                ChangeGameState(EGameState.PreConnect);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving authenticationDenied in game state {_currentGameState}");  
                
            }
        }
        private void HandleReceivedPlayRequest(string opponentUsername)
        {
            if (_currentGameState == EGameState.Authenticated)
            {
                UIManager.Instance.DeactivateUIScreen(UIManager.UIState.CreatePlayRequest);
                ChangeGameState(EGameState.AnsweringPlayRequest);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving authenticationDenied in game state {_currentGameState}");  
                
            }
        }
        private void HandleReceivedDenyPlayRequest(DenyPlayRequestMessage.EDenialReason denialReason)
        {
            if (_currentGameState == EGameState.WaitingForAnswerToPlayRequest)
            {
                ChangeGameState(EGameState.Authenticated);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving DenyPlayRequest in game state {_currentGameState}");
            }
        }
        private void HandleGameStarted(string opponentName, EGameRole playerGameRole)
        {
            if (_currentGameState == EGameState.WaitingForAnswerToPlayRequest)
            {
                ChangeGameState(EGameState.LoadingPlaySession);
                InGameInputHandler.SetPlayerGameRole(playerGameRole);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving GameStarted in game state {_currentGameState}");
            }
        }
        private void HandleInGameSceneLoaded(AsyncOperation asyncOperation)
        {
            asyncOperation.allowSceneActivation = true;
            ClientManager.ConfirmGameIsLoaded();
        }
        private void HandleAllPlayersLoadedGame()
        {
            ChangeGameState(EGameState.InPlaySession);
        }
        private void HandleGameEnded(MatchData gameResult)
        {
            ChangeGameState(EGameState.InResultsScreen);
        }

        //================== User Input Handling ===============
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
                    HandleAcceptPlayRequestInput();
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
                    HandleDeclinePlayRequestInput();
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
        private void HandleUsernameInput(string username)
        {
            if (_currentGameState == EGameState.Unauthenticated)
            {
                ClientManager.Authenticate(username);
            }
            else
            {
                Debug.LogWarning($"Should not be triggering usernameInputEvent in game state {_currentGameState}");            
            }
        }
        private void HandleCreatePlayRequestInput()
        {
            if (_currentGameState == EGameState.Authenticated)
            {
                UIManager.Instance.ActivateUIScreen(UIManager.UIState.CreatePlayRequest, true);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving CreatePlayRequestInput in game state {_currentGameState}");  
                
            }
        }
        private void HandleAbortCreatePlayRequestInput()
        {
            if (_currentGameState == EGameState.Authenticated)
            {
                UIManager.Instance.DeactivateUIScreen(UIManager.UIState.CreatePlayRequest);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving AbortCreatePlayRequestInput in game state {_currentGameState}");  
                
            }
        }
        private void HandleRequestPlayWithRandomInput()
        {
            if (_currentGameState == EGameState.Authenticated)
            {
                ChangeGameState(EGameState.WaitingForAnswerToPlayRequest);
                ClientManager.RequestPlayWithRandom();
            }
            else
            {
                Debug.LogWarning($"Should not be receiving RequestPlayWithRandomInput in game state {_currentGameState}");
            }
        }
        private void HandleRequestPlayWithSessionCodeInput(string opponentSessionCode)
        {
            if (_currentGameState == EGameState.Authenticated)
            {
                ChangeGameState(EGameState.WaitingForAnswerToPlayRequest);
                ClientManager.RequestPlayWithSessionCode(opponentSessionCode);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving RequestPlayWithSessionCodeInput in game state {_currentGameState}");
            }
        }
        private void HandlePlayAgainInput()
        {
            if (_currentGameState == EGameState.InResultsScreen)
            {
                ChangeGameState(EGameState.WaitingForAnswerToPlayRequest);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving PlayAgainInput in game state {_currentGameState}");
            }
        }
        private void HandleReturnToMainMenuInput()
        {
            if (_currentGameState == EGameState.InResultsScreen)
            {
                ChangeGameState(EGameState.Authenticated);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving ReturnToMainMenuInput in game state {_currentGameState}");
            }
        }
        private void HandleAcceptPlayRequestInput()
        {
            if (_currentGameState == EGameState.AnsweringPlayRequest)
            {
                ClientManager.AcceptPlayRequest();
                ChangeGameState(EGameState.LoadingPlaySession);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving AcceptPlayRequestInput in game state {_currentGameState}");  
                
            }
        }
        private void HandleDeclinePlayRequestInput()
        {
            if (_currentGameState == EGameState.AnsweringPlayRequest)
            {
                ClientManager.DenyPlayRequest();
                ChangeGameState(EGameState.Authenticated);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving DeclinePlayRequestInput in game state {_currentGameState}");  
                
            }
        }
        private void HandleQuitInput()
        {
            if (_currentGameState != EGameState.InPlaySession 
                && _currentGameState != EGameState.LoadingPlaySession)
            {
                if(_currentGameState == EGameState.AnsweringPlayRequest)
                    HandleDeclinePlayRequestInput();
                SceneManager.QuitApplication();
            }
            else
            {
                Debug.LogWarning("may not arbitrarily quit while in play session");
            }
        }
        
        //================== InGame User Input Handling ===============
        private void HandleMoleLookedInput(EHoleIndex holeIndex)
        {
            if (_currentGameState == EGameState.InPlaySession)
            {
                ClientManager.SendMoleLook(holeIndex);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving MoleLookedInput in game state {_currentGameState}");  
                
            }
        }
        private void HandleMoleHidInput()
        {
            if (_currentGameState == EGameState.InPlaySession)
            {
                ClientManager.SendMoleHide();
            }
            else
            {
                Debug.LogWarning($"Should not be receiving MoleHidInput in game state {_currentGameState}");  
                
            }
        }
        private void HandleHitterHitInput(Vector2 position)
        {
            if (_currentGameState == EGameState.InPlaySession)
            {
                ClientManager.SendHitterHit(position);
            }
            else
            {
                Debug.LogWarning($"Should not be receiving HitterHitInput in game state {_currentGameState}");  
                
            }
        }
        
        //=================== Functionality ==========================
        private void ChangeGameState(EGameState gameState)
        {
            Debug.Log($"Game State Changed: {gameState}");
            _currentGameState = gameState;
            switch (gameState)
            {
                case EGameState.PreConnect when SceneManager.IsSceneLoadedOrLoading(Scenes.Networking.Index()):
                    ReloadNetworking();
                    break;
                case EGameState.PreConnect:
                    LoadNetworking();
                    break;
                case EGameState.Connecting:
                    LoadUISceneIfNotAlreadyLoaded(
                        () => UIManager.Instance.ActivateUIScreen(UIManager.UIState.Connecting));
                    break;
                case EGameState.Unauthenticated:
                    var coroutine = LoadAuthenticateScreenInASecond();
                    StartCoroutine(coroutine);
                    break;
                case EGameState.Authenticated:
                    UIManager.Instance.ActivateUIScreen(UIManager.UIState.MainMenu);
                    break;
                case EGameState.WaitingForAnswerToPlayRequest:
                    UIManager.Instance.ActivateUIScreen(UIManager.UIState.WaitForPlayRequestResponse, true);
                    break;
                case EGameState.AnsweringPlayRequest:
                    UIManager.Instance.ActivateUIScreen(UIManager.UIState.RespondToPlayRequest, true);
                    break;
                case EGameState.LoadingPlaySession:
                    UIManager.Instance.ActivateUIScreen(UIManager.UIState.Loading);
                    SceneManager.LoadSceneAsync(Scenes.InGame.Index(), HandleInGameSceneLoaded, LoadSceneMode.Additive);
                    break;
                case EGameState.InPlaySession:
                    UIManager.Instance.ActivateUIScreen(UIManager.UIState.InGame);
                    break;
                case EGameState.InResultsScreen:
                    UIManager.Instance.ActivateUIScreen(UIManager.UIState.GameResults);
                    SceneManager.UnloadSceneAsync(Scenes.InGame.Index());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerator LoadAuthenticateScreenInASecond()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            UIManager.Instance.ActivateUIScreen(UIManager.UIState.UsernameInput);
        }

        private void LoadUISceneIfNotAlreadyLoaded([CanBeNull] Action onUISceneLoadedAction)
        {
            if (!SceneManager.IsSceneLoadedOrLoading(Scenes.UI.Index()))
            {
                SceneManager.LoadSceneAsync(Scenes.UI.Index(), operation =>
                    {
                        operation.allowSceneActivation = true;
                        onUISceneLoadedAction?.Invoke();
                    },
                    LoadSceneMode.Additive);
            }
            else
            {
                onUISceneLoadedAction?.Invoke();
            }
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

        

        
    }
    
    
}