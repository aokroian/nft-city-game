using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using DataClasses;
using Server.Dto;
using Server.Dto.Inventory;
using UnityEngine;

public class ServerApi : AbstractServerApi
{
    public override void LoadFullSave(Action<FullSaveDto> result, Action<ResponseError> error)
    {
        throw new NotImplementedException();
    }

    public override void GetHouseData(int houseId, Action<InventoryBuildingDto> result, Action<ResponseError> error)
    {
        throw new NotImplementedException();
    }

    public override void BuyOrUpgradeHouse(int houseId, Action<InventoryBuildingDto> result, Action<ResponseError> error)
    {
        throw new NotImplementedException();
    }

    public override void CheckIfHouseBuilt(int houseId, Action<InventoryBuildingDto> result, Action<ResponseError> error)
    {
        throw new NotImplementedException();
    }

    public override void CollectResource(ResourceItem resourceItem, Action<bool> result, Action<ResponseError> error)
    {
        throw new NotImplementedException();
    }

    public override void GetCurrentEnergy(Action<int> result, Action<ResponseError> error)
    {
        throw new NotImplementedException();
    }

    public override void GetCurrentCoins(Action<float> result, Action<ResponseError> error)
    {
        throw new NotImplementedException();
    }
}
