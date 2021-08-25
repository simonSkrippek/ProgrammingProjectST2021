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
            Connecting = 0,
            MainMenu = 1,
            Loading = 2,
            InGame = 3,
            GameResultsUI = 4,
            UserStatsUI = 5,
        }

        [SerializeField] private UIStateUIScreenControllerDictionary uiDict;
        private UIState _currentUIState;

        private void OnValidate()
        {
            var all_values = Enum.GetValues(typeof(UIState)).Cast<UIState>().ToArray();

            foreach (var value in all_values)
            {
                if(!uiDict.ContainsKey(value))
                    uiDict.Add(value, null);
            }
        }

        public void ActivateUIScreen(UIState state)
        {
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
    }
}