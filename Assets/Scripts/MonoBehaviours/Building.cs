using System;
using DataClasses;
using Events;
using Server.Dto;
using Server.Dto.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace MonoBehaviours
{
    public class Building : Interactable
    {
        #region Inspector

        [Space(15)] [Header("SETTINGS")] [Space(5)]
        public int houseID;


        [Space(15)] [Header("REFS")] [Space(5)]
        public Transform visualsContainer;

        public GameObject buildingMenu;

        public GameObject infoPlate;

        public IntGameEvent requestHouseDataEvent;

        [Space(15)] [Header("INTERNAL EVENTS")] [Space(5)]
        public UnityEvent<BuildingItem> onSetBuildingData;

        [Space(15)] [Header("HOUSE DATA DISPLAY")] [Space(5)]
        public BuildingItem buildingItem;

        [Space(15)] [Header("OTHERS")] [Space(5)]

        #endregion

        #region Fields

        public bool isMenuOpen = false;

        #endregion

        #region MonoBehaviour

        public void RequestHouseDataWithEvent()
        {
            requestHouseDataEvent.Raise(houseID);
        }

        private void Awake()
        {
            onSetBuildingData ??= new UnityEvent<BuildingItem>();
            buildingItem ??= new BuildingItem();
        }

        public void SetBuildingData(InventoryBuildingDto inventoryBuildingDto)
        {
            if (inventoryBuildingDto == null)
            {
                throw new ArgumentNullException(nameof(inventoryBuildingDto), "house receiver null houseDto");
            }

            if (inventoryBuildingDto.inventoryID == houseID)
            {
                buildingItem = new BuildingItem(inventoryBuildingDto: inventoryBuildingDto);
                onSetBuildingData.Invoke(buildingItem);
            }
        }

        public void ReloadVisuals()
        {
            foreach (Transform t in visualsContainer)
            {
                // t.gameObject.SetActive(t.name == "Tier" + (buildingItem.tier));
                // if (buildingItem.isBought)
                // {
                //     t.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
                // }
                // else
                // {
                //     t.gameObject.GetComponentInChildren<MeshRenderer>().material.color = new Color(0.2f, 0.2f, 0.2f);
                // }
            }
        }


        public void SetFullSaveData(FullSaveDto fullSaveDto)
        {
            if (fullSaveDto == null)
            {
                Debug.LogWarning("house receiver null fullSaveDto", this);
                return;
            }

            var houses = fullSaveDto.houses;
            foreach (var houseDto in houses)
            {
                if (houseDto.inventoryID == houseID)
                {
                    buildingItem.id = houseDto.inventoryID;
                    buildingItem.tier = houseDto.tier;
                    // buildingItem.isBought = houseDto.isBought;
                    buildingItem.buildTimer = houseDto.buildStartTime;
                    buildingItem.upgradeTimer = houseDto.upgradeStartTime;
                }
            }

            onSetBuildingData.Invoke(buildingItem);
        }

        public void ToggleBuildingMenu()
        {
            buildingMenu.SetActive(!isMenuOpen);
            isMenuOpen = !isMenuOpen;
        }

        public void CheckIsHovered()
        {
            infoPlate.SetActive(IsHovered());
        }

        #endregion
    }
}