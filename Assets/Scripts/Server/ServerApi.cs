using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerApi : AbstractServerApi
{
    public override void GetHouseData(int houseId, Action<HouseDto> result, Action<ResponseError> error)
    {
        throw new NotImplementedException();
    }
}
