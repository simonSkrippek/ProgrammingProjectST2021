using System;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using WhackAStoodent.Helper;
using WhackAStoodent.UI.DataClasses;

namespace WhackAStoodent.UI
{
    public class UIManager : ASingletonManagerScript<UIManager>
    {
        public enum UIState
        {
            None = 0,
            Connecting = 1,
            Loading = 2,
            UsernameInput = 3,
            MainMenu = 4,
            RespondToPlayRequest = 5,
            CreatePlayRequest = 5,
            WaitForPlayRequestResponse = 5,
            InGame = 14,
            GameResults = 15,
            UserStatsUI = 20,
        }

        [SerializeField] private UIStateUIScreenControllerDictionary uiDict;
        private UIState _currentUIState = UIState.None;

        private void OnValidate()
        {
            var all_values = Enum.GetValues(typeof(UIState)).Cast<UIState>().ToArray();

            foreach (var value in all_values)
            {
                if(!uiDict.ContainsKey(value))
                    uiDict.Add(value, null);
            }
        }

        public void ActivateUIScreen(UIState state, bool displayAdditively = false)
        {
            //TODO handle additive ui
            if(state == _currentUIState) return;
            
            foreach (var pair in uiDict)
            {
                if (pair.Key != state)
                {
                    pair.Value.Deactivate();
                }
                else
                {
                    pair.Value.Activate();
                }
            }
        }
        
        public void DeactivateUIScreen(UIState state)
        {
            //TODO
        }
    }
}