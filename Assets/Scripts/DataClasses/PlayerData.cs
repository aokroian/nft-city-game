using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace DataClasses
{
    [Serializable]
    public class PlayerData
    {
        public PlayerData(int energyAmount = 0, float coinsAmount = 0f, int energyToCollect = 0, Dictionary<ResourceType, int> currentResources = null)
        {
            this.energyAmount = energyAmount;
            this.coinsAmount = coinsAmount;
            this.energyToCollect = energyToCollect;
            this.currentResources = currentResources;
        }

        public PlayerData(FullSaveDto fullSaveDto)
        {
            energyAmount = fullSaveDto.energy;
            coinsAmount = fullSaveDto.coins;
            energyToCollect = fullSaveDto.energyToCollect;
            currentResources = fullSaveDto.currentResources;
        }
        public int energyAmount;
        public float coinsAmount;
        public int energyToCollect;
        public Dictionary<ResourceType, int> currentResources;
    }
}