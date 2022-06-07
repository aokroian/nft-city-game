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
    public Transform visualsContainer;

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
        if (houseDto == null)
        {
            Debug.LogWarning("house receiver null houseDto", this);
            return;
        }
        if (houseDto.id == houseID)
        {
            buildingData = new BuildingData(houseDto: houseDto);
            onSetBuildingData.Invoke(buildingData);
        }
    }

    public void ReloadVisuals()
    {
        foreach (Transform t in visualsContainer)
        {
            t.gameObject.SetActive(t.name == "Tier" + (buildingData.tier));
            if (buildingData.isBought)
            {
                t.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
            }
            else
            {
                t.gameObject.GetComponentInChildren<MeshRenderer>().material.color = new Color(0.2f, 0.2f, 0.2f);
            }
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
            if (houseDto.id == houseID)
            {
                buildingData.id = houseDto.id;
                buildingData.tier = houseDto.tier;
                buildingData.isBought = houseDto.isBought;
                buildingData.buildTimer = houseDto.buildTimer;
                buildingData.upgradeTimer = houseDto.upgradeTimer;
            }
        }
        onSetBuildingData.Invoke(buildingData);
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