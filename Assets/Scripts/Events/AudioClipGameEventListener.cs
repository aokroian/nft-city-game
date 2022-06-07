using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class AudioClipGameEventListener : MonoBehaviour
    {
        public AudioClipGameEvent eventForListen;
        public UnityEvent<AudioClip> response;

        private void OnEnable()
        {
            eventForListen.AddListener(this);
        }

        private void OnDisable()
        {
            eventForListen.RemoveListener(this);
        }

        public void OnEventRaised(AudioClip arg)
        {
            response.Invoke(arg);
        }
    }
}