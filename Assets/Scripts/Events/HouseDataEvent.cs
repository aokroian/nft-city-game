using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "HouseDataEvent", menuName = "GameEvents/HouseDataEvent", order = 0)]
    public class HouseDataEvent : ScriptableObject
    {
        private List<HouseDataEventListener> listeners = new();

        public void Raise(HouseDto arg)
        {
            // Backwards for easy removing listener inside listener itself
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(arg);
            }
        }

        public void AddListener(HouseDataEventListener listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(HouseDataEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}