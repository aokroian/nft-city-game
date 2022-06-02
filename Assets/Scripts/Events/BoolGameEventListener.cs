using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class BoolGameEventListener : MonoBehaviour
    {
        public BoolGameEvent eventForListen;
        public UnityEvent<bool> response;

        private void OnEnable()
        {
            eventForListen.AddListener(this);
        }

        private void OnDisable()
        {
            eventForListen.RemoveListener(this);
        }

        public void OnEventRaised(bool arg)
        {
            response.Invoke(arg);
        }
    }
}