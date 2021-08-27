using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhackAStoodent.Events
{
    [CreateAssetMenu(fileName = "Event", menuName = "Custom/Events/NoParameterEvent", order = 0)]
    public class NoParameterEvent : ScriptableObject
    {
        private List<Action> _subscriptionList;
        
        private void OnEnable()
        {
            _subscriptionList = new List<Action>();
        }

        public void Subscribe(Action action)
        {
            _subscriptionList.Add(action);
        }

        public void Unsubscribe(Action action)
        {
            _subscriptionList.Remove(action);
        }

        public void Invoke()
        {
            for (var index = _subscriptionList.Count - 1; index >= 0; index--)
            {
                var subscription = _subscriptionList[index];
                subscription.Invoke();
            }
        }
    }
}