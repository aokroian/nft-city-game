using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractServerApi: MonoBehaviour
{
    public abstract void getHouseData(int houseId, Action<HouseDto> result, Action<ResponseError> error);
}
