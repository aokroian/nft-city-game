using System.Collections.Generic;

namespace Server.Dto
{
    public class PlayerDto
    {
        public string name;
        public string email;
        public InventoryDto inventory;
        public InventoryResourceDto[] spawnedResources;
    }
}