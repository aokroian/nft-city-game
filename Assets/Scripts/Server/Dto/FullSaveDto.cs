using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullSaveDto
{
    public float coins;
    public int energy;
    public HouseDto[] houses;
    public Dictionary<ResourceType, int> mapResources;
    public Dictionary<ResourceType, int> currentResources;
}
