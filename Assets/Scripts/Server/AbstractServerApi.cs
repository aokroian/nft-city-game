using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractServerApi: MonoBehaviour
{
    public abstract void LoadFullSave(Action<FullSaveDto> result, Action<ResponseError> error);

    public abstract void GetHouseData(int houseId, Action<HouseDto> result, Action<ResponseError> error);

    public abstract void BuyOrUpgradeHouse(int houseId, Action<HouseDto> result, Action<ResponseError> error);

    public abstract void CheckIfHouseBuilt(int houseId, Action<HouseDto> result, Action<ResponseError> error);
}
