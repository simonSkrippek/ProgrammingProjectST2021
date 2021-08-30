using System;
using System.Collections.Generic;
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
            CreatePlayRequest = 5,
            WaitForPlayRequestResponse = 6,
            RespondToPlayRequest = 7,
            InGame = 14,
            GameResults = 15,
            UserStatsUI = 20,
        }

        [SerializeField] private UIStateUIScreenControllerDictionary uiDict;
        private UIState _currentBaseUIState = UIState.None;

        private void OnValidate()
        {
            var all_values = Enum.GetValues(typeof(UIState)).Cast<UIState>().ToArray();

            foreach (var value in all_values)
            {
                if(!uiDict.ContainsKey(value))
                    uiDict.Add(value, null);
            }

            foreach (Transform child in transform)
            {
                if (TryGetComponent<Canvas>(out Canvas canvas))
                {
                    canvas.sortingOrder = child.GetSiblingIndex();
                }
            }
        }
        
        public void ActivateUIScreen(UIState state, bool displayAdditively = false)
        {
            //TODO handle additive ui
            if(state == _currentBaseUIState) return;
            
            if(displayAdditively == false)
            {
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
                _currentBaseUIState = state;
            }
            else if(uiDict.TryGetValue(state, out UIScreenController controller_to_activate))
            {
                controller_to_activate.Activate();
            }
        }
        
        public void DeactivateUIScreen(UIState state)
        {
            //TODO
            if (state == _currentBaseUIState)
            {
                ActivateUIScreen(UIState.None);
            }
            else if(uiDict.TryGetValue(state, out UIScreenController controller_to_deactivate))
            {
                controller_to_deactivate.Deactivate();
            }
        }
    }
}