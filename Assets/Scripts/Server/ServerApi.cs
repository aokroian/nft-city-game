using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerApi : AbstractServerApi
{
    public override void LoadFullSave(Action<FullSaveDto> result, Action<ResponseError> error)
    {
        throw new NotImplementedException();
    }

    public override void GetHouseData(int houseId, Action<HouseDto> result, Action<ResponseError> error)
    {
        throw new NotImplementedException();
    }

    public override void BuyOrUpgradeHouse(int houseId, Action<HouseDto> result, Action<ResponseError> error)
    {
        throw new NotImplementedException();
    }

    public override void CheckIfHouseBuilt(int houseId, Action<HouseDto> result, Action<ResponseError> error)
    {
        throw new NotImplementedException();
    }
}
