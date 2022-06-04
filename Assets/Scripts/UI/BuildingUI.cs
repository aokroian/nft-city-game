using System;
using DataClasses;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BuildingUI : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private Building building;

        [Space(15)] [Header("IN WORLD REFS")] [Space(5)] [SerializeField]
        private TextMeshProUGUI hoverPlate;

        [SerializeField] private GameObject brokenIcon;
        [SerializeField] private TextMeshProUGUI timerPlate;

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

        #endregion


        #region Fields

        private BuildingData _data;

        #endregion

        #region MonoBehaviour

        private void Start()
        {
            building ??= GetComponentInParent<Building>();
        }

        public void ReloadUI()
        {
            _data = building.buildingData;
            // in world part
            hoverPlate.text = $"House {_data?.tier} tier";

            // menu part
            menuTitle.text = $"House {_data?.tier} tier";
            tier.text = $"Tier: {_data?.tier}";
            status.text = $"Status: {_data?.status}";
            citizensCount.text = $"Citizens inside: {_data?.citizensCount}";
            dailyClaim.text = $"Daily claim: {_data?.dailyClaim:0.0000}";
            lastClaim.text = $"Last claim: {_data?.lastClaim:0.0000}";
            totalClaim.text = $"Total claim: {_data?.totalClaim:0.0000}";
            minClaim.text = $"Min claim: {_data?.minClaim:0.0000}";
            vault.text = $"Vault: {_data?.vault:0.0000}";
        }

        #endregion
    }
}