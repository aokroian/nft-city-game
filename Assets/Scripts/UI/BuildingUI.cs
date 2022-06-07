using System.Linq;
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
        [SerializeField] private IntGameEvent claimHouseVaultEvent;

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
        [SerializeField] private Slider vaultSlider;
        [SerializeField] private GameObject vaultContainer;

        [SerializeField] private Button claimButton;

        [SerializeField] private Button buyButton;
        [SerializeField] private Button upgradeByCoinsButton;
        [SerializeField] private Button upgradeByResourcesButton;

        #endregion


        #region Fields

        private BuildingData _buildingData;
        private PlayerData _playerData;

        private TextMeshProUGUI _buyButtonTMP;
        private TextMeshProUGUI _upgradeWithCoinsButtonTMP;
        private TextMeshProUGUI _upgradeWithResourcesButtonTMP;

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
            vault.text = $"Vault: {_buildingData?.vault:0.00}%";
            if (_buildingData != null)
            {
                vaultSlider.value = _buildingData.vault;
                vaultContainer.SetActive(_buildingData.isBought);
            }

            // menu buttons 

            claimButton.gameObject.SetActive(_buildingData is {isBought: true} &&
                                             _buildingData.vault >= _buildingData.minClaim);
            buyButton.gameObject.SetActive(_buildingData is {isBought: false});
            upgradeByCoinsButton.gameObject.SetActive(_buildingData is {isBought: true, tier: < 3});
            upgradeByResourcesButton.gameObject.SetActive(_buildingData is {isBought: true, tier: < 3});

            buyButton.interactable = _buildingData?.allowedToBuy ?? false;
            upgradeByCoinsButton.interactable = _buildingData?.allowedToUpgradeByCoins ?? false;
            upgradeByResourcesButton.interactable = _buildingData?.allowedToUpgradeByResources ?? false;


            _buyButtonTMP ??= buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _upgradeWithCoinsButtonTMP ??= upgradeByCoinsButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _upgradeWithResourcesButtonTMP ??= upgradeByResourcesButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();


            _buyButtonTMP.text =
                $"Buy for {_buildingData?.buyCoinsCost:0.0000} coins";


            _upgradeWithCoinsButtonTMP.text =
                $"Upgrade for {_buildingData?.upgradeCost:0.0000} coins";

            var woodPart = _buildingData?.upgradeResourceCost?[ResourceType.Wood] + " wood ";
            var stonePart = _buildingData?.upgradeResourceCost?[ResourceType.Stone] + " stone ";
            var ironPart = _buildingData?.upgradeResourceCost?[ResourceType.Iron] + " iron ";
            _upgradeWithResourcesButtonTMP.text = "Upgrade for " + woodPart + stonePart + ironPart;
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

        public void RaiseClaimHouseVaultEvent()
        {
            _buildingData = building.buildingData;
            claimHouseVaultEvent.Raise(_buildingData.id);
        }

        #endregion
    }
}