using System;
using System.Collections.Generic;
using Enums;

namespace DataClasses
{
    [Serializable]
    public class PlayerData
    {
        public PlayerData(int energyAmount = 0, float coinsAmount = 0f, Dictionary<ResourceType, int> currentResources = null)
        {
            this.energyAmount = energyAmount;
            this.coinsAmount = coinsAmount;
            this.currentResources = currentResources;
        }

        public PlayerData(FullSaveDto fullSaveDto)
        {
            energyAmount = fullSaveDto.energy;
            coinsAmount = fullSaveDto.coins;
            currentResources = fullSaveDto.currentResources;
        }
        public int energyAmount;
        public float coinsAmount;
        public Dictionary<ResourceType, int> currentResources;
    }
}