using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhackAStoodent.Events
{
    public class TwoParameterEvent<T1, T2> : ScriptableObject
    {
        private List<Action<T1, T2>> _subscriptionList;

        private void OnEnable()
        {
            _subscriptionList = new List<Action<T1, T2>>();
        }

        public void Subscribe(Action<T1, T2> action)
        {
            _subscriptionList.Add(action);
        }

        public void Unsubscribe(Action<T1, T2> action)
        {
            _subscriptionList.Remove(action);
        }

        public void Invoke(T1 parameter1, T2 parameter2)
        {
            for (var index = _subscriptionList.Count - 1; index >= 0; index--)
            {
                var subscription = _subscriptionList[index];
                subscription?.Invoke(parameter1, parameter2);
            }
        }
    }
}