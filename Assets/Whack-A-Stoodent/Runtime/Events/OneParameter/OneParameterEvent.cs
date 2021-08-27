using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhackAStoodent.Events
{
    public class OneParameterEvent<T> : ScriptableObject
    {
        private List<Action<T>> _subscriptionList;

        private void OnEnable()
        {
            _subscriptionList = new List<Action<T>>();
        }

        public void Subscribe(Action<T> action)
        {
            _subscriptionList.Add(action);
        }

        public void Unsubscribe(Action<T> action)
        {
            _subscriptionList.Remove(action);
        }

        public void Invoke(T parameter)
        {
            for (var index = _subscriptionList.Count - 1; index >= 0; index--)
            {
                var subscription = _subscriptionList[index];
                subscription?.Invoke(parameter);
            }
        }
    }
}