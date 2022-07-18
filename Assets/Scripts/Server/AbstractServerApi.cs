using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using DataClasses;
using Server.Dto;
using Server.Dto.Inventory;
using UnityEngine;

public abstract class AbstractServerApi: MonoBehaviour
{
    public abstract void LoadFullSave(Action<FullSaveDto> result, Action<ResponseError> error);

    public abstract void GetHouseData(int houseId, Action<InventoryBuildingDto> result, Action<ResponseError> error);

    public abstract void BuyOrUpgradeHouse(int houseId, Action<InventoryBuildingDto> result, Action<ResponseError> error);

    // Maybe use GetHouseData instead?
    public abstract void CheckIfHouseBuilt(int houseId, Action<InventoryBuildingDto> result, Action<ResponseError> error);

    public abstract void CollectResource(ResourceItem resourceItem, Action<bool> result, Action<ResponseError> error);

    public abstract void GetCurrentEnergy(Action<int> result, Action<ResponseError> error);

    public abstract void GetCurrentCoins(Action<float> result, Action<ResponseError> error);
}
