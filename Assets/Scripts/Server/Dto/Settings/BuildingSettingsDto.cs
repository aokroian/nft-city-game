namespace Server.Dto.Settings
{
    public class BuildingSettingsDto
    {
        public int settingsID;
        public string buildingName;

        public int maxCitizens;
        public float dailyClaimByCitizen;
        public float vaultSize;
        public float minClaim;
        
        public int tier;
        public float upgradeWithCoinsCost;
        public InventoryResourceDto[] upgradeWithResourcesCost;
        public BuildingSettingsDto upgradedVersion;
        public int upgradeTime;

        public float buyWithCoinsCost;
        public int buildTime;

        public float fullRepairWithCoinsCost;
    }
}