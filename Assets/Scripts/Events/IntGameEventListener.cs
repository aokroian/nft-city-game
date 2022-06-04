using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class IntGameEventListener : MonoBehaviour
    {
        public IntGameEvent eventForListen;
        public UnityEvent<int> response;

        private void OnEnable()
        {
            eventForListen.AddListener(this);
        }

        private void OnDisable()
        {
            eventForListen.RemoveListener(this);
        }

        public void OnEventRaised(int arg)
        {
            response.Invoke(arg);
        }
    }
}