using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers
{
        private static PointerEventData _eventDataCurrentPosition;
        private static List<RaycastResult> _result;

        public static bool IsOverUi()
        {
                _eventDataCurrentPosition = new PointerEventData(EventSystem.current) {position = Input.mousePosition};
                _result = new List<RaycastResult>();
                EventSystem.current.RaycastAll(_eventDataCurrentPosition, _result);
                return _result.Count > 0;
        }
}