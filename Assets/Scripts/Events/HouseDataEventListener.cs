using Server.Dto;
using Server.Dto.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class HouseDataEventListener : MonoBehaviour
    {
        public HouseDataEvent eventForListen;
        public UnityEvent<InventoryBuildingDto> response;

        private void OnEnable()
        {
            eventForListen.AddListener(this);
        }

        private void OnDisable()
        {
            eventForListen.RemoveListener(this);
        }

        public void OnEventRaised(InventoryBuildingDto arg)
        {
            response.Invoke(arg);
        }
    }
}