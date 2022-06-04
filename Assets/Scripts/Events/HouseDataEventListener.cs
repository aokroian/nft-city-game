using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class HouseDataEventListener : MonoBehaviour
    {
        public HouseDataEvent eventForListen;
        public UnityEvent<HouseDto> response;

        private void OnEnable()
        {
            eventForListen.AddListener(this);
        }

        private void OnDisable()
        {
            eventForListen.RemoveListener(this);
        }

        public void OnEventRaised(HouseDto arg)
        {
            response.Invoke(arg);
        }
    }
}