using Enums;

namespace Server.Dto.Inventory
{
    public class InventoryBuildingDto
    {
        public int settingsID;
        public int inventoryID;
        public HouseStatus status;
        public int tier;
        public int citizensCount;

        public float lastCoinsAddedTime;
        public float lastClaim;
        public float totalClaim;
        public float currentVault;
        
        public float buildStartTime;
        public float upgradeStartTime;
        public InventoryBuildingVisuals visuals;
    }
}
