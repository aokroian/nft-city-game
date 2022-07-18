using DataClasses;
using Enums;
using Events;
using MonoBehaviours;
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

        private BuildingItem _buildingItem;
        private PlayerInventory _playerInventory;

        private TextMeshProUGUI _buyButtonTMP;
        private TextMeshProUGUI _upgradeWithCoinsButtonTMP;
        private TextMeshProUGUI _upgradeWithResourcesButtonTMP;

        #endregion

        #region MonoBehaviour

        private void Start()
        {
            building ??= GetComponentInParent<Building>();
            _playerInventory ??= FindObjectOfType<PlayerInventory>();
        }

        public void SetPlayerData(FullSaveDto fullSaveDto)
        {
            _playerInventory.UpdateInventory(fullSaveDto); 
        }

        public void ReloadUI()
        {
            _buildingItem = building.buildingItem;
            // in world part
            hoverPlate.text = $"House {_buildingItem?.tier} tier";
            if (_buildingItem?.buildTimer > 0)
            {
                hoverPlate.text += $"\n{_buildingItem?.buildTimer:0} sec to build";
            }

            if (_buildingItem?.upgradeTimer > 0)
            {
                hoverPlate.text += $"\n{_buildingItem?.upgradeTimer:0} sec to upgrade";
            }

            // if (_buildingData?.status == HouseStatus.Bad)
            // {
            //     hoverPlate.text += "\n Broken";
            // }

            // menu part
            menuTitle.text = $"House {_buildingItem?.tier} tier";
            tier.text = $"Tier: {_buildingItem?.tier}";
            status.text = $"Status: {_buildingItem?.status}";
            citizensCount.text = $"Citizens inside: {_buildingItem?.citizensCount}";
            dailyClaim.text = $"Daily claim: {_buildingItem?.dailyClaim:0.0000}";
            lastClaim.text = $"Last claim: {_buildingItem?.lastClaim:0.0000}";
            totalClaim.text = $"Total claim: {_buildingItem?.totalClaim:0.0000}";
            minClaim.text = $"Min claim: {_buildingItem?.minClaim:0.0000}";
            vault.text = $"Vault: {_buildingItem?.vault:0.00}%";
            if (_buildingItem != null)
            {
                vaultSlider.value = _buildingItem.vault;
                // vaultContainer.SetActive(_buildingItem.isBought);
            }

            // menu buttons 

            // claimButton.gameObject.SetActive(_buildingItem is {isBought: true} &&
                                             // _buildingItem.vault >= _buildingItem.minClaim);
            // buyButton.gameObject.SetActive(_buildingItem is {isBought: false});
            // upgradeByCoinsButton.gameObject.SetActive(_buildingItem is {isBought: true, tier: < 3});
            // upgradeByResourcesButton.gameObject.SetActive(_buildingItem is {isBought: true, tier: < 3});

            buyButton.interactable = _buildingItem?.allowedToBuy ?? false;
            upgradeByCoinsButton.interactable = _buildingItem?.allowedToUpgradeByCoins ?? false;
            upgradeByResourcesButton.interactable = _buildingItem?.allowedToUpgradeByResources ?? false;


            _buyButtonTMP ??= buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _upgradeWithCoinsButtonTMP ??= upgradeByCoinsButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _upgradeWithResourcesButtonTMP ??= upgradeByResourcesButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();


            _buyButtonTMP.text =
                $"Buy for {_buildingItem?.buyCoinsCost:0.0000} coins";


            _upgradeWithCoinsButtonTMP.text =
                $"Upgrade for {_buildingItem?.upgradeCost:0.0000} coins";

            var woodPart = _buildingItem?.upgradeResourceCost?[ResourceType.Wood] + " wood ";
            var stonePart = _buildingItem?.upgradeResourceCost?[ResourceType.Stone] + " stone ";
            var ironPart = _buildingItem?.upgradeResourceCost?[ResourceType.Iron] + " iron ";
            _upgradeWithResourcesButtonTMP.text = "Upgrade for " + woodPart + stonePart + ironPart;
        }

        public void RaiseBuyHouseEvent()
        {
            _buildingItem = building.buildingItem;
            buyHouseEvent.Raise(_buildingItem.id);
        }

        public void RaiseUpgradeHouseWithCoinsEvent()
        {
            _buildingItem = building.buildingItem;
            upgradeHouseWithCoinsEvent.Raise(_buildingItem.id);
        }

        public void RaiseUpgradeHouseWithResourcesEvent()
        {
            _buildingItem = building.buildingItem;
            upgradeHouseWithResourcesEvent.Raise(_buildingItem.id);
        }

        public void RaiseClaimHouseVaultEvent()
        {
            _buildingItem = building.buildingItem;
            claimHouseVaultEvent.Raise(_buildingItem.id);
        }

        #endregion
    }
}