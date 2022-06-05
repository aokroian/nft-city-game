using DataClasses;
using Enums;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuildingUI : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private Building building;

        [Space(15)] [Header("IN WORLD REFS")] [Space(5)] [SerializeField]
        private TextMeshProUGUI hoverPlate;

        [SerializeField] private IntGameEvent buyHouseEvent;
        [SerializeField] private IntGameEvent upgradeHouseWithCoinsEvent;
        [SerializeField] private IntGameEvent upgradeHouseWithResourcesEvent;

        [Space(15)] [Header("MENU REFS")] [Space(5)] [SerializeField]
        private TextMeshProUGUI menuTitle;

        [SerializeField] private TextMeshProUGUI tier;
        [SerializeField] private TextMeshProUGUI status;
        [SerializeField] private TextMeshProUGUI citizensCount;
        [SerializeField] private TextMeshProUGUI dailyClaim;
        [SerializeField] private TextMeshProUGUI lastClaim;
        [SerializeField] private TextMeshProUGUI totalClaim;
        [SerializeField] private TextMeshProUGUI minClaim;
        [SerializeField] private TextMeshProUGUI vault;

        [SerializeField] private Button buyButton;
        [SerializeField] private Button upgradeByCoinsButton;
        [SerializeField] private Button upgradeByResourcesButton;

        #endregion


        #region Fields

        private BuildingData _buildingData;
        private PlayerData _playerData;

        #endregion

        #region MonoBehaviour

        private void Start()
        {
            building ??= GetComponentInParent<Building>();
            _playerData ??= new PlayerData();
        }

        public void SetPlayerData(FullSaveDto fullSaveDto)
        {
            _playerData = new PlayerData(fullSaveDto: fullSaveDto);
        }

        public void ReloadUI()
        {
            _buildingData = building.buildingData;
            // in world part
            hoverPlate.text = $"House {_buildingData?.tier} tier";
            if (_buildingData?.buildTimer > 0)
            {
                hoverPlate.text += $"\n{_buildingData?.buildTimer:0} sec to build";
            }

            if (_buildingData?.upgradeTimer > 0)
            {
                hoverPlate.text += $"\n{_buildingData?.upgradeTimer:0} sec to upgrade";
            }

            // if (_buildingData?.status == HouseStatus.Bad)
            // {
            //     hoverPlate.text += "\n Broken";
            // }

            // menu part
            menuTitle.text = $"House {_buildingData?.tier} tier";
            tier.text = $"Tier: {_buildingData?.tier}";
            status.text = $"Status: {_buildingData?.status}";
            citizensCount.text = $"Citizens inside: {_buildingData?.citizensCount}";
            dailyClaim.text = $"Daily claim: {_buildingData?.dailyClaim:0.0000}";
            lastClaim.text = $"Last claim: {_buildingData?.lastClaim:0.0000}";
            totalClaim.text = $"Total claim: {_buildingData?.totalClaim:0.0000}";
            minClaim.text = $"Min claim: {_buildingData?.minClaim:0.0000}";
            vault.text = $"Vault: {_buildingData?.vault:0.0000}";

            // menu buttons 
            buyButton.gameObject.SetActive(_buildingData is {isBought: false});
            upgradeByCoinsButton.gameObject.SetActive(_buildingData is {isBought: true, tier: < 3});
            upgradeByResourcesButton.gameObject.SetActive(_buildingData is {isBought: true, tier: < 3});

            buyButton.interactable = _buildingData?.allowedToBuy ?? false;
            upgradeByCoinsButton.interactable = _buildingData?.allowedToUpgradeByCoins ?? false;
            upgradeByResourcesButton.interactable = _buildingData?.allowedToUpgradeByResources ?? false;

            buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                $"Buy {_buildingData?.buyCoinsCost:0.0000} coins";
        }

        public void RaiseBuyHouseEvent()
        {
            _buildingData = building.buildingData;
            buyHouseEvent.Raise(_buildingData.id);
        }

        public void RaiseUpgradeHouseWithCoinsEvent()
        {
            _buildingData = building.buildingData;
            upgradeHouseWithCoinsEvent.Raise(_buildingData.id);
        }

        public void RaiseUpgradeHouseWithResourcesEvent()
        {
            _buildingData = building.buildingData;
            upgradeHouseWithResourcesEvent.Raise(_buildingData.id);
        }

        #endregion
    }
}