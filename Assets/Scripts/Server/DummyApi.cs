using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine.Events;

namespace Server
{
    public class DummyApi : AbstractServerApi
    {
        public UnityEvent<FullSaveDto> onFullSaveLoaded;
        public UnityEvent<HouseDto> onHouseDataLoaded;

        public int LoadCost => 100;

        private FullSaveDto _fullSave;

        private void Awake()
        {
            _fullSave = CreateFullSaveDto();
            AddHousesToDto();
            onFullSaveLoaded ??= new UnityEvent<FullSaveDto>();
            onHouseDataLoaded ??= new UnityEvent<HouseDto>();
        }


        public void LoadFullSave()
        {
            LoadFullSave(dto => { onFullSaveLoaded.Invoke(dto); }, err => { });
        }

        public void GetHouseData(int houseId)
        {
            GetHouseData(houseId, dto => { onHouseDataLoaded.Invoke(dto); }, err => { });
        }

        public override void LoadFullSave(Action<FullSaveDto> result, Action<ResponseError> error)
        {
            result(_fullSave);
        }

        public override void GetHouseData(int houseId, Action<HouseDto> result, Action<ResponseError> error)
        {
            if (_fullSave?.houses?.Length > 0)
            {
                var houseData = _fullSave.houses.ToList().Find(houseDto => houseDto.id == houseId);
                result(houseData);
            }
        }

        public override void BuyOrUpgradeHouse(int houseId, Action<HouseDto> result, Action<ResponseError> error)
        {
            var house = _fullSave.houses.ToList().Find(houseDto => houseDto.id == houseId);
            if (house != null)
            {
                house.isBought = true;
                house.buyCoinsCost++;
                house.upgradeCost++;
                result(house);
            }
        }

        public override void CheckIfHouseBuilt(int houseId, Action<HouseDto> result, Action<ResponseError> error)
        {
            var house = _fullSave.houses.ToList().Find(houseDto => houseDto.id == houseId);
            if (house != null)
            {
                result(house);
            }
            
        }

        public override void CollectResource(ResourceType type, Action<bool> result, Action<ResponseError> error)
        {
            if (_fullSave.mapResources[type] > 0)
            {
                _fullSave.mapResources[type]--;
                result(true);
            }
            else
            {
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

        private FullSaveDto CreateFullSaveDto()
        {
            var fullSaveDto = new FullSaveDto
            {
                coins = 8.567f,
                energy = 55,
                houses = new HouseDto[3],
                mapResources = new Dictionary<ResourceType, int>
                {
                    {ResourceType.Stone, 4},
                    {ResourceType.Wood, 6},
                    {ResourceType.Iron, 3}
                },
                currentResources = new Dictionary<ResourceType, int>
                {
                    {ResourceType.Stone, 4},
                    {ResourceType.Wood, 6},
                    {ResourceType.Iron, 3}
                }
            };
            return fullSaveDto;
        }

        private void AddHousesToDto()
        {
            _fullSave.houses[0] = CreateHouseDto(1, HouseStatus.Average, 0);
            _fullSave.houses[1] = CreateHouseDto(2, HouseStatus.Bad, 1);
            _fullSave.houses[2] = CreateHouseDto(3, HouseStatus.Good, 2);
        }

        private HouseDto CreateHouseDto(int id, HouseStatus status, int tier)
        {
            var newHouseDto = new HouseDto()
            {
                id = id,
                isBought = true,
                status = status,
                tier = tier,
                citizens = 1,
                dailyclaim = 0.2345f,
                lastClaim = 0.1234f,
                minClaim = 30,
                totalClaim = 1.6789f,
                vault = 13.52f,
                upgradeCost = 3.0581f,
                buyCoinsCost = 3.0581f,
                buildTimer = 5.0f,
            };

            var upgradeResourceCostDictionary = new Dictionary<ResourceType, int>
            {
                {ResourceType.Iron, 1},
                {ResourceType.Stone, 1},
                {ResourceType.Wood, 1}
            };
            newHouseDto.upgradeResourceCost = upgradeResourceCostDictionary;
            newHouseDto.allowedToBuy = CheckIfAllowedToBuyHouse(newHouseDto);
            newHouseDto.allowedToUpgradeByCoins = CheckIfAllowedToUpgradeByCoins(newHouseDto);
            newHouseDto.allowedToUpgradeByResources = CheckIfAllowedToUpgradeHouseByResources(newHouseDto);
            return newHouseDto;
        }

        private bool CheckIfAllowedToBuyHouse(HouseDto houseDto)
        {
            return _fullSave.coins >= houseDto.buyCoinsCost;
        }

        private bool CheckIfAllowedToUpgradeByCoins(HouseDto houseDto)
        {
            return _fullSave.coins >= houseDto.upgradeCost;
        }

        private bool CheckIfAllowedToUpgradeHouseByResources(HouseDto houseDto)
        {
            var isAllowed = true;
            foreach (var resource in houseDto.upgradeResourceCost)
            {
                var current = _fullSave.currentResources;
                var checkResource = current.ToList().Find(r => r.Key == resource.Key);
                if (checkResource.Value < resource.Value)
                {
                    isAllowed = false;
                }
            }

            return isAllowed;
        }
    }
}