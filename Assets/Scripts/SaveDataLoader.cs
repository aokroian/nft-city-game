using DataClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataLoader : MonoBehaviour, Loadable
{
    public ServerApiProvider apiProvider;
    public GameObject housesParent;

    public int loadCost => 100;

    public void Load(Action onLoad)
    {
        apiProvider.concreteApi.LoadFullSave(data => 
        {
            // TODO: FILL ENERGY AND COINS DATA!!!

            FillAllHosesData(data);
        },
        error =>
        {
            Debug.Log("Full save loading failed! " + error.message);
        });
    }

    private void FillAllHosesData(FullSaveDto fullData)
    {
        foreach (var house in housesParent.GetComponentsInChildren<BuildingMainData>())
        {
            foreach (var houseData in fullData.houses)
            {
                if (house.id == houseData.id)
                {
                    house.status = houseData.status;
                    house.isBought = houseData.isBought;
                    house.tier = houseData.tier;
                    house.timer = houseData.buildTimer;
                }
            }
        }
    }
}
