using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "IntGameEvent", menuName = "GameEvents/IntGameEvent", order = 0)]
    public class IntGameEvent : ScriptableObject
    {
        private List<IntGameEventListener> listeners = new();

        public void Raise(int arg)
        {
            // Backwards for easy removing listener inside listener itself
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(arg);
            }
        }

        public void AddListener(IntGameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(IntGameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}