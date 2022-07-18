using Enums;
using System.Collections;
using System.Collections.Generic;
using Server.Dto;
using Server.Dto.Inventory;
using UnityEngine;

public class FullSaveDto
{
    public float coins;
    public int energy;
    public InventoryBuildingDto[] houses;
    public Dictionary<ResourceType, int> mapResources;
    public Dictionary<ResourceType, int> currentResources;
}
