using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Enums;
using UnityEngine.Events;

public class DummyApi : AbstractServerApi
{
    public UnityEvent<FullSaveDto> onFullSaveLoaded;
    public UnityEvent<HouseDto> onHouseDataLoaded;

    public int loadCost => 100;

    private void Awake()
    {
        onFullSaveLoaded ??= new UnityEvent<FullSaveDto>();
        onHouseDataLoaded ??= new UnityEvent<HouseDto>();
    }


    private FullSaveDto fullSave = new FullSaveDto
    {
        coins = 8.567f,
        energy = 55f,
        houses = new HouseDto[]
        {
            new HouseDto
            {
                id = 0,
                isBought = true,
                status = HouseStatus.Good,
                tier = 0,
                citizens = 1,
                dailyclaim = 0.2345f,
                lastClaim = 0.1234f,
                minClaim = 30,
                totalClaim = 1.6789f,
                vault = 13.52f,
                upgradeCost = 3.0581f,
                buildTimer = 5.0f
            },
            new HouseDto
            {
                id = 1,
                isBought = true,
                status = HouseStatus.Good,
                tier = 1,
                citizens = 1,
                dailyclaim = 0.2345f,
                lastClaim = 0.1234f,
                minClaim = 30,
                totalClaim = 1.6789f,
                vault = 13.52f,
                upgradeCost = 3.0581f,
                buildTimer = 5.0f
            },
            new HouseDto
            {
                id = 2,
                isBought = true,
                status = HouseStatus.Average,
                tier = 2,
                citizens = 3,
                dailyclaim = 0.4345f,
                lastClaim = 0.2234f,
                minClaim = 30,
                totalClaim = 5.6789f,
                vault = 45.52f,
                upgradeCost = 5.0581f,
                buildTimer = 0f
            }
        }
    };

    public void LoadFullSave()
    {
        LoadFullSave(dto => { onFullSaveLoaded.Invoke(dto); }, err => { });
    }

    public void GetHouseData(int houseId)
    {
        GetHouseData(houseId, dto => { onHouseDataLoaded.Invoke(dto); }, err => { });
    }

    public override void LoadFullSave(Action<FullSaveDto> result, Action<ResponseError> error)
    {
        result(fullSave);
    }

    public override void GetHouseData(int houseId, Action<HouseDto> result, Action<ResponseError> error)
    {
        if (fullSave.houses.Length > 0)
        {
            var houseData = fullSave.houses.ToList().Find(houseDto => houseDto.id == houseId);
            result(houseData);
        }
    }

    public override void BuyOrUpgradeHouse(int houseId, Action<HouseDto> result, Action<ResponseError> error)
    {
        var houseData = new HouseDto
        {
            id = houseId,
            isBought = true,
            status = HouseStatus.Good,
            tier = 1,
            citizens = 1,
            dailyclaim = 0.2345f,
            lastClaim = 0.1234f,
            minClaim = 30,
            totalClaim = 1.6789f,
            vault = 13.52f,
            upgradeCost = 5.0581f,
            buildTimer = 5.0f
        };
        result(houseData);
    }

    public override void CheckIfHouseBuilt(int houseId, Action<HouseDto> result, Action<ResponseError> error)
    {
        var houseData = new HouseDto
        {
            id = houseId,
            isBought = true,
            status = HouseStatus.Good,
            tier = 1,
            citizens = 1,
            dailyclaim = 0.2345f,
            lastClaim = 0.1234f,
            minClaim = 30,
            totalClaim = 1.6789f,
            vault = 13.52f,
            upgradeCost = 5.0581f,
            buildTimer = 5.0f
        };
        result(houseData);
    }
}