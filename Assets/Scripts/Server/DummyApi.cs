using System;
using System.Collections.Generic;
using System.Linq;
using DataClasses;
using Enums;
using Server.Dto;
using Server.Dto.Inventory;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Server
{
    public class DummyApi : AbstractServerApi
    {
        public UnityEvent<FullSaveDto> onFullSaveLoaded;
        public UnityEvent<InventoryBuildingDto> onHouseDataLoaded;

        public int LoadCost => 100;

        private static FullSaveDto _fullSave;

        private void Awake()
        {
            _fullSave = CreateFullSaveDto();
            AddHousesToFullSaveDto(_fullSave);
            onFullSaveLoaded ??= new UnityEvent<FullSaveDto>();
            onHouseDataLoaded ??= new UnityEvent<InventoryBuildingDto>();
        }


        #region MethodsForInspector

        public void LoadFullSave()
        {
            LoadFullSave(dto => { onFullSaveLoaded.Invoke(dto); }, err => { });
        }

        public void LoadFullSave(FullSaveDto newFullSave)
        {
            if (newFullSave == null)
            {
                LoadFullSave();
                return;
            }

            _fullSave = newFullSave;
            LoadFullSave(dto => { onFullSaveLoaded.Invoke(dto); }, err => { });
        }

        public void GetHouseData(int houseId)
        {
            GetHouseData(houseId, dto => { onHouseDataLoaded.Invoke(dto); }, err => { });
        }

        public void BuyHouse(int houseId)
        {
            BuyOrUpgradeHouse(houseId, dto => { }, error => { });
            foreach (var houseDto in _fullSave.houses)
            {
                if (houseDto.inventoryID == houseId && _fullSave.coins >= houseDto.buyCoinsCost/* && !houseDto.isBought*/)
                {
                    // houseDto.isBought = true;
                    _fullSave.coins -= houseDto.buyCoinsCost;
                }
            }

            LoadFullSave();
        }

        public void UpgradeHouseWithCoins(int houseId)
        {
            foreach (var houseDto in _fullSave.houses)
            {
                if (houseDto.inventoryID == houseId && CheckIfAllowedToUpgradeByCoins(houseDto, _fullSave)/* &&
                    houseDto.isBought*/)
                {
                    houseDto.tier++;
                    houseDto.upgradeCost++;
                    houseDto.upgradeResourceCost[ResourceType.Iron] += 1;
                    houseDto.upgradeResourceCost[ResourceType.Wood] += 1;
                    houseDto.upgradeResourceCost[ResourceType.Stone] += 1;

                    _fullSave.coins -= houseDto.upgradeCost;

                    houseDto.allowedToUpgradeByCoins = CheckIfAllowedToUpgradeByCoins(houseDto, _fullSave);
                    houseDto.allowedToUpgradeByResources =
                        CheckIfAllowedToUpgradeHouseByResources(houseDto, fullSaveDto: _fullSave);
                    
                    BuyOrUpgradeHouse(houseId, dto => { }, error => { });
                    LoadFullSave(_fullSave);
                    GetHouseData(houseId);
                }
            }
        }

        public void UpgradeHouseWithResources(int houseId)
        {
            foreach (var houseDto in _fullSave.houses)
            {
                if (houseDto.inventoryID == houseId &&
                    CheckIfAllowedToUpgradeHouseByResources(houseDto, fullSaveDto: _fullSave)/* && houseDto.isBought*/)
                {
                    houseDto.tier++;
                    houseDto.upgradeCost++;
                    houseDto.upgradeResourceCost[ResourceType.Iron] += 1;
                    houseDto.upgradeResourceCost[ResourceType.Wood] += 1;
                    houseDto.upgradeResourceCost[ResourceType.Stone] += 1;

                    
                    _fullSave.currentResources[ResourceType.Iron] -= 1;
                    _fullSave.currentResources[ResourceType.Wood] -= 1;
                    _fullSave.currentResources[ResourceType.Stone] -= 1;

                    houseDto.allowedToUpgradeByResources =
                        CheckIfAllowedToUpgradeHouseByResources(houseDto, fullSaveDto: _fullSave);
                    houseDto.allowedToUpgradeByCoins = CheckIfAllowedToUpgradeByCoins(houseDto, _fullSave);
                    
                    
                    BuyOrUpgradeHouse(houseId, dto => { }, error => { });
                    LoadFullSave(_fullSave);
                    GetHouseData(houseId);
                }
            }
        }

        public void ClaimHouseVault(int houseId)
        {
            foreach (var houseDto in _fullSave.houses)
            {
                if (houseDto.inventoryID == houseId &&
                    CheckIfAllowedToClaim(houseDto) /*&& houseDto.isBought*/)
                {
                    float receivedCoins = (float) (houseDto.dailyclaim * houseDto.currentVault * 0.01);
                    houseDto.lastClaim = receivedCoins;
                    houseDto.totalClaim += receivedCoins;
                    _fullSave.coins += receivedCoins;
                    houseDto.currentVault = 0;

                    LoadFullSave(_fullSave);
                    GetHouseData(houseId);
                }
            }
        }

        #endregion


        public override void LoadFullSave(Action<FullSaveDto> result, Action<ResponseError> error)
        {
            result(_fullSave);
        }

        public override void GetHouseData(int houseId, Action<InventoryBuildingDto> result, Action<ResponseError> error)
        {
            if (_fullSave?.houses?.Length > 0)
            {
                var houseData = _fullSave.houses.ToList().Find(houseDto => houseDto.inventoryID == houseId);
                result(houseData);
            }
        }

        public override void BuyOrUpgradeHouse(int houseId, Action<InventoryBuildingDto> result, Action<ResponseError> error)
        {
            // result(house);
        }

        public override void CheckIfHouseBuilt(int houseId, Action<InventoryBuildingDto> result, Action<ResponseError> error)
        {
            var house = _fullSave.houses.ToList().Find(houseDto => houseDto.inventoryID == houseId);
            if (house != null)
            {
                result(house);
            }
        }

        public override void CollectResource(ResourceItem resourceItem, Action<bool> result, Action<ResponseError> error)
        {
            if (_fullSave.mapResources[resourceItem.resourceType] > 0 && _fullSave.energy >= resourceItem.collectEnergyCost)
            {
                // Debug.Log("COLLECT");
                _fullSave.mapResources[resourceItem.resourceType]--;
                _fullSave.energy -= resourceItem.collectEnergyCost;
                _fullSave.currentResources[resourceItem.resourceType]++;
                result(true);
                LoadFullSave(_fullSave);
            }
            else
            {
                // Debug.Log("NOT COLLECT " + type + " " + _fullSave.mapResources[type] + " " + _fullSave.energy + " " + _fullSave.energyToCollect);
                result(false);
            }
        }

        public override void GetCurrentEnergy(Action<int> result, Action<ResponseError> error)
        {
            result(_fullSave.energy);
        }

        public override void GetCurrentCoins(Action<float> result, Action<ResponseError> error)
        {
            result(_fullSave.coins);
        }

        #region Methods To Create Dto

        private static FullSaveDto CreateFullSaveDto()
        {
            var fullSaveDto = new FullSaveDto
            {
                coins = 10.567f,
                energy = 55,
                houses = new InventoryBuildingDto[19],
                mapResources = new Dictionary<ResourceType, int>
                {
                    {ResourceType.Stone, 4},
                    {ResourceType.Wood, 6},
                    {ResourceType.Iron, 3}
                },
                currentResources = new Dictionary<ResourceType, int>
                {
                    {ResourceType.Stone, 2},
                    {ResourceType.Wood, 2},
                    {ResourceType.Iron, 2}
                }
            };
            // Debug.Log("11111111111 - " + fullSaveDto.mapResources.Count() + " " + fullSaveDto.mapResources[ResourceType.Iron]);
            return fullSaveDto;
        }

        private static void AddHousesToFullSaveDto(FullSaveDto fullSaveDto)
        {
            for (var i = 0; i < fullSaveDto.houses.Length; i++)
            {
                fullSaveDto.houses[i] = CreateHouseDto(i, false, (HouseStatus)Random.Range(0, 3), Random.Range(1, 4));
            }
        }

        private static InventoryBuildingDto CreateHouseDto(int id, bool isBought, HouseStatus status, int tier)
        {
            var newHouseDto = new InventoryBuildingDto()
            {
                inventoryID = id,
                // isBought = isBought,
                status = status,
                tier = tier,
                citizensCount = 1,
                dailyclaim = 0.2345f,
                lastClaim = 0.1234f,
                minClaim = 30,
                totalClaim = 1.6789f,
                currentVault = 35.52f,
                upgradeCost = 3.0581f,
                buyCoinsCost = 3.0581f,
                buildStartTime = 0f,
            };

            var upgradeResourceCostDictionary = new Dictionary<ResourceType, int>
            {
                {ResourceType.Iron, 1},
                {ResourceType.Stone, 1},
                {ResourceType.Wood, 1}
            };
            newHouseDto.upgradeResourceCost = upgradeResourceCostDictionary;
            newHouseDto.allowedToBuy = CheckIfAllowedToBuyHouse(newHouseDto, fullSaveDto: _fullSave);
            newHouseDto.allowedToUpgradeByCoins = CheckIfAllowedToUpgradeByCoins(newHouseDto, fullSaveDto: _fullSave);
            newHouseDto.allowedToUpgradeByResources =
                CheckIfAllowedToUpgradeHouseByResources(newHouseDto, fullSaveDto: _fullSave);
            return newHouseDto;
        }

        #endregion

        #region Methods To Calculate Fields

        private static bool CheckIfAllowedToBuyHouse(InventoryBuildingDto inventoryBuildingDto, FullSaveDto fullSaveDto)
        {
            return fullSaveDto.coins >= inventoryBuildingDto.buyCoinsCost;
        }

        private static bool CheckIfAllowedToUpgradeByCoins(InventoryBuildingDto inventoryBuildingDto, FullSaveDto fullSaveDto)
        {
            return fullSaveDto.coins >= inventoryBuildingDto.upgradeCost && inventoryBuildingDto.tier < 3;
        }

        private static bool CheckIfAllowedToUpgradeHouseByResources(InventoryBuildingDto inventoryBuildingDto, FullSaveDto fullSaveDto)
        {
            var isAllowed = true;
            foreach (var resource in inventoryBuildingDto.upgradeResourceCost)
            {
                var current = fullSaveDto.currentResources;
                var checkResource = current.ToList().Find(r => r.Key == resource.Key);
                if (checkResource.Value < resource.Value)
                {
                    isAllowed = false;
                }
            }

            if (inventoryBuildingDto.tier >= 3) isAllowed = false;
            return isAllowed;
        }

        private static bool CheckIfAllowedToClaim(InventoryBuildingDto inventoryBuildingDto)
        {
            return inventoryBuildingDto.currentVault >= inventoryBuildingDto.minClaim;
        }

        #endregion
    }
}