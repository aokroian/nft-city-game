using System.Collections.Generic;
using Newtonsoft.Json;
using Server.Dto.Inventory;

namespace Server.Dto
{
    public class InventoryDto
    {
        public int energy;
        public float coins;
        public InventoryResourceDto[] resources;
        public InventoryToolDto[] tools;
        public InventoryBuildingDto[] buildings;
    }
}