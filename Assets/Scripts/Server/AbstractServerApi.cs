using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractServerApi: MonoBehaviour
{
    public abstract void LoadFullSave(Action<FullSaveDto> result, Action<ResponseError> error);

    public abstract void GetHouseData(int houseId, Action<HouseDto> result, Action<ResponseError> error);

    public abstract void BuyOrUpgradeHouse(int houseId, Action<HouseDto> result, Action<ResponseError> error);

    // Maybe use GetHouseData instead?
    public abstract void CheckIfHouseBuilt(int houseId, Action<HouseDto> result, Action<ResponseError> error);

    public abstract void CollectResource(ResourceType type, Action<bool> result, Action<ResponseError> error);

    public abstract void GetCurrentEnergy(Action<int> result, Action<ResponseError> error);

    public abstract void GetCurrentCoins(Action<float> result, Action<ResponseError> error);
}
