using System;
using DataClasses;
using Events;
using UnityEngine;
using UnityEngine.Events;


public class Building : Interactable
{
    #region Inspector
    
    [Space(15)] [Header("SETTINGS")] [Space(5)]
    public int houseID;
    

    [Space(15)] [Header("REFS")] [Space(5)]
    public GameObject buildingMenu;

    public GameObject infoPlate;

    public IntGameEvent requestHouseDataEvent;

    [Space(15)] [Header("INTERNAL EVENTS")] [Space(5)]

    public UnityEvent<BuildingData> onSetBuildingData;

    [Space(15)] [Header("HOUSE DATA DISPLAY")] [Space(5)]
    public BuildingData buildingData;
    
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
        // onDisable ??= new UnityEvent();
        // onEnable ??= new UnityEvent();
        onSetBuildingData ??= new UnityEvent<BuildingData>();
        buildingData ??= new BuildingData();
    }

    public void SetBuildingData(HouseDto houseDto)
    {
        if (houseDto.id == houseID)
        {
            buildingData = new BuildingData()
            {
                id = houseDto.id,
                isBought = houseDto.isBought,
                status = houseDto.status,
                buildTimer = houseDto.buildTimer,
                upgradeTimer = houseDto.upgradeTimer,
                citizensCount = houseDto.citizens,
                tier = houseDto.tier,
                dailyClaim = houseDto.dailyclaim,
                lastClaim = houseDto.lastClaim,
                totalClaim = houseDto.totalClaim,
                minClaim = houseDto.minClaim,
                vault = houseDto.vault,
                upgradeCost = houseDto.upgradeCost
            };
            onSetBuildingData.Invoke(buildingData);
        }
        
    }

    public void SetFullSaveData(FullSaveDto fullSaveDto)
    {
        var houses = fullSaveDto.houses;
        foreach (var houseDto in houses)
        {
            if (houseDto.id == houseID)
            {
                buildingData.id = houseDto.id;
                buildingData.isBought = houseDto.isBought;
                buildingData.buildTimer = houseDto.buildTimer;
                buildingData.upgradeTimer = houseDto.upgradeTimer;
            }
        }
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