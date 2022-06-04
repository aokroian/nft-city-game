using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class DummyApi : AbstractServerApi
{
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
                id = 1,
                isBought = false,
                upgradeCost = 1.2f,
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

    public override void LoadFullSave(Action<FullSaveDto> result, Action<ResponseError> error)
    {
        result(fullSave);
    }

    public override void GetHouseData(int houseId, Action<HouseDto> result, Action<ResponseError> error)
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
