using System;
using UnityEngine;

namespace UI
{
    public class ButtonWithToggleVisuals : MonoBehaviour
    {
        [SerializeField] private GameObject enabledIcon;
        [SerializeField] private GameObject disabledIcon;
        public bool initialState = true;

        private bool _currentState = false;
        private void Start()
        {
            _currentState = initialState;
            enabledIcon.SetActive(initialState);
            disabledIcon.SetActive(!initialState);
        }

        public void Toggle()
        {
            enabledIcon.SetActive(!_currentState);
            disabledIcon.SetActive(!enabledIcon.activeSelf);
            _currentState = !_currentState;
        }
    }
}