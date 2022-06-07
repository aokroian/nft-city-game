using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "AudioClipGameEvent", menuName = "GameEvents/AudioClipGameEvent", order = 0)]
    public class AudioClipGameEvent : ScriptableObject
    {
        private List<AudioClipGameEventListener> listeners = new();

        public void Raise(AudioClip arg)
        {
            // Backwards for easy removing listener inside listener itself
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(arg);
            }
        }

        public void AddListener(AudioClipGameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(AudioClipGameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}