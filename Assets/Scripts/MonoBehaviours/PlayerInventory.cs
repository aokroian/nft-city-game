using System;
using System.Collections.Generic;
using DataClasses;
using Enums;
using Newtonsoft.Json;
using Server.Dto;
using UnityEngine;
using UnityEngine.Windows;

namespace MonoBehaviours
{
    public class PlayerInventory : MonoBehaviour
    {
        public int Energy { get; private set; }
        public float Coins { get; private set; }
        public List<ResourceItem> Resources { get; private set; }
        public List<ToolItem> Tools { get; private set; }
        public List<BuildingItem> Buildings { get; private set; }

        private void Awake()
        {
            // var player = new PlayerDto();
            // var inventory = new InventoryDto();
            // var resources = new ResourceDto[]
            // {
            //     new ResourceDto()
            // };
            // var buildings = new BuildingDto[]
            // {
            //     new BuildingDto()
            // };
            // player.inventory = inventory;
            // player.inventory.buildings = buildings;
            // player.inventory.resources = resources;
            // player.inventory.coins = 3234.23243f;
            // var testString = JsonConvert.SerializeObject(player);
            //
            // var path = Application.dataPath + "/Resources/PlayerTest.json";
            // System.IO.File.WriteAllText(path, testString);
        }

        public void UpdateInventory(FullSaveDto fullSaveDto)
        {
            Energy = fullSaveDto.energy;
            Coins = fullSaveDto.coins;
            // Resources = fullSaveDto.currentResources;
        }

        public void AddEnergy(int addedAmount)
        {
            Energy += addedAmount;
        }

        public void AddCoins(float addedAmount)
        {
            Coins += addedAmount;
        }

        public void AddResource()
        {
        }

        public void AddTool()
        {
        }

        public void RemoveTools()
        {
        }

        public void AddBuilding()
        {
        }

        public void RemoveBuilding()
        {
        }

        public void UpdateBuilding()
        {
        }
    }
}