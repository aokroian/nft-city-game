using System;
using Enums;
using UnityEngine;

namespace DataClasses
{
    [Serializable]
    public class BuildingData
    {
        public BuildingData(HouseDto houseDto = null)
        {
            if (houseDto == null) return;
            id = houseDto.id;
            isBought = houseDto.isBought;
            status = houseDto.status;
            buildTimer = houseDto.buildTimer;
            upgradeTimer = houseDto.upgradeTimer;
            citizensCount = houseDto.citizens;
            tier = houseDto.tier;
            dailyClaim = houseDto.dailyclaim;
            lastClaim = houseDto.lastClaim;
            totalClaim = houseDto.totalClaim;
            minClaim = houseDto.minClaim;
            vault = houseDto.vault;
            upgradeCost = houseDto.upgradeCost;
            allowedToBuy = houseDto.allowedToBuy;
            allowedToUpgradeByCoins = houseDto.allowedToUpgradeByCoins;
            allowedToUpgradeByResources = houseDto.allowedToUpgradeByResources;
            buyCoinsCost = houseDto.buyCoinsCost;
            buildTimer = houseDto.buildTimer;
            upgradeTimer = houseDto.upgradeTimer;
        }


        public int id;
        public bool isBought;
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
    }
}