using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "BoolGameEvent", menuName = "GameEvents/BoolGameEvent", order = 0)]
    public class BoolGameEvent : ScriptableObject
    {
        private List<BoolGameEventListener> listeners = new();

        public void Raise(bool arg)
        {
            // Backwards for easy removing listener inside listener itself
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(arg);
            }
        }

        public void AddListener(BoolGameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(BoolGameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}