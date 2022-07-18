using System;
using System.Collections.Generic;
using Enums;
using Server.Dto;
using Server.Dto.Inventory;
using UnityEngine;

namespace DataClasses
{
    [Serializable]
    public class BuildingItem
    {
        public BuildingItem(InventoryBuildingDto inventoryBuildingDto = null)
        {
            if (inventoryBuildingDto == null) return;
            id = inventoryBuildingDto.inventoryID;
            status = inventoryBuildingDto.status;
            buildTimer = inventoryBuildingDto.buildStartTime;
            upgradeTimer = inventoryBuildingDto.upgradeStartTime;
            citizensCount = inventoryBuildingDto.citizensCount;
            tier = inventoryBuildingDto.tier;
            dailyClaim = inventoryBuildingDto.dailyclaim;
            lastClaim = inventoryBuildingDto.lastClaim;
            totalClaim = inventoryBuildingDto.totalClaim;
            minClaim = inventoryBuildingDto.minClaim;
            vault = inventoryBuildingDto.currentVault;
            upgradeCost = inventoryBuildingDto.upgradeCost;
            allowedToBuy = inventoryBuildingDto.allowedToBuy;
            allowedToUpgradeByCoins = inventoryBuildingDto.allowedToUpgradeByCoins;
            allowedToUpgradeByResources = inventoryBuildingDto.allowedToUpgradeByResources;
            buyCoinsCost = inventoryBuildingDto.buyCoinsCost;
            buildTimer = inventoryBuildingDto.buildStartTime;
            upgradeTimer = inventoryBuildingDto.upgradeStartTime;
            upgradeResourceCost = inventoryBuildingDto.upgradeResourceCost;
        }


        public int id;
        public HouseStatus status;
        public float buildTimer;
        public float upgradeTimer;
        public int citizensCount;
        public int tier;
        public float dailyClaim;
        public float lastClaim;
        public float totalClaim;
        public int minClaim;
        public float vault;
        public float upgradeCost;
        public bool allowedToBuy;
        public bool allowedToUpgradeByResources;
        public bool allowedToUpgradeByCoins;
        public float buyCoinsCost;
        public Dictionary<ResourceType, int> upgradeResourceCost;
    }
}