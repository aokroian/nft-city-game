using System;
using Enums;
using UnityEngine;

namespace DataClasses
{
    [Serializable]
    public class BuildingData
    {
        public BuildingData(int id = 0, bool isBought = false, HouseStatus status = HouseStatus.Good,
            float buildTimer = -1f, float upgradeTimer = -1f,
            int citizensCount = 0, int tier = -1, float dailyClaim = 0f, float lastClaim = 0f, float totalClaim = 0f,
            int minClaim = 0, float vault = 0, float upgradeCost = 0, bool allowedToBuy = false,
            bool allowedToUpgradeByCoins = false, bool allowedToUpgradeByResources = false, float buyCoinsCost = 0f)
        {
            this.id = id;
            this.isBought = isBought;
            this.status = status;
            this.buildTimer = buildTimer;
            this.upgradeTimer = upgradeTimer;
            this.citizensCount = citizensCount;
            this.tier = tier;
            this.dailyClaim = dailyClaim;
            this.lastClaim = lastClaim;
            this.totalClaim = totalClaim;
            this.minClaim = minClaim;
            this.vault = vault;
            this.upgradeCost = upgradeCost;
            this.allowedToBuy = allowedToBuy;
            this.allowedToUpgradeByCoins = allowedToUpgradeByCoins;
            this.allowedToUpgradeByResources = allowedToUpgradeByResources;
            this.buyCoinsCost = buyCoinsCost;
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