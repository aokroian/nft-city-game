using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "FullSaveDataEvent", menuName = "GameEvents/FullSaveDataEvent", order = 0)]
    public class FullSaveDataEvent : ScriptableObject
    {
        private List<FullSaveDataEventListener> listeners = new();

        public void Raise(FullSaveDto arg)
        {
            // Backwards for easy removing listener inside listener itself
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(arg);
            }
        }

        public void AddListener(FullSaveDataEventListener listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(FullSaveDataEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}