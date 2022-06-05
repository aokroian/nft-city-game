using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class HouseDto
{
    public int id;
    public bool isBought;
    public HouseStatus status;
    public int tier;
    public int citizens;
    public float dailyclaim;
    public float lastClaim;
    public float totalClaim;
    public int minClaim;
    public float vault;
    public float upgradeCost;
    public Dictionary<ResourceType, int> upgradeResourceCost;
    public float buyCoinsCost;
    public float buildTimer;
    public float upgradeTimer;
    public bool allowedToBuy;
    public bool allowedToUpgradeByCoins;
    public bool allowedToUpgradeByResources;
}
