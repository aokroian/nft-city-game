using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class FullSaveDataEventListener : MonoBehaviour
    {
        public FullSaveDataEvent eventForListen;
        public UnityEvent<FullSaveDto> response;

        private void OnEnable()
        {
            eventForListen.AddListener(this);
        }

        private void OnDisable()
        {
            eventForListen.RemoveListener(this);
        }

        public void OnEventRaised(FullSaveDto arg)
        {
            response.Invoke(arg);
        }
    }
}