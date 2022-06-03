using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyApi : AbstractServerApi
{
    public override void GetHouseData(int houseId, Action<HouseDto> result, Action<ResponseError> error)
    {
        var houseData = new HouseDto
        {
            id = houseId,
            houseStatus = HouseStatus.Good,
            rank = 1,
            citizens = 1,
            dailyclaim = 0.2345f,
            lastClaim = 0.1234f,
            minClaim = 30,
            totalClaim = 1.6789f,
            vault = 13.52f
        };
        result(houseData);
    }
}
