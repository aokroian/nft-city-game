using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    [Serializable]
    public class EventWithDelayObject
    {
        public string name = "EventName";
        public float delay;
        public UnityEvent onDoEvent;
    }

    public class EventWithDelay : MonoBehaviour
    {
        public bool triggerOnStart;
        public bool triggerOnEnable;

        [SerializeField] private EventWithDelayObject @event;
        [Space(100)] private bool _all;
        private int _eventIndex;

        private void OnEnable()
        {
            if (triggerOnEnable)
            {
                DoEvent();
            }
        }
        private void Start()
        {
            if (triggerOnStart)
            {
                DoEvent();
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }


        public void DoEvent()
        {
            StartCoroutine(DoEventWithDelay(@event));
        }
        
        static IEnumerator DoEventWithDelay(EventWithDelayObject @event)
        {
            yield return new WaitForSeconds(@event.delay);
            @event.onDoEvent.Invoke();
        }
    }
}