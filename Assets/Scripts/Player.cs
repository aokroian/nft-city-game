using System;
using DataClasses;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
        [Space(15)] [Header("PLAYER DATA DISPLAY")] [Space(5)]
        public PlayerData playerData;

        [Space(15)] [Header("HUD REFS")] [Space(5)] [SerializeField]
        private TextMeshProUGUI energyField;

        [SerializeField] private TextMeshProUGUI coinsField;
        [SerializeField] private TextMeshProUGUI woodField;
        [SerializeField] private TextMeshProUGUI stoneField;
        [SerializeField] private TextMeshProUGUI ironField;
        

        [Space(15)] [Header("EVENTS")] [Space(5)]
        public UnityEvent<PlayerData> onSetPlayerData;

        private void Awake()
        {
                onSetPlayerData ??= new UnityEvent<PlayerData>();
                playerData ??= new PlayerData();
                // Debug.Log("energyToCollect = " + playerData.energyToCollect);
    }

        public void SetPlayerData(FullSaveDto fullSaveDto)
        {
                if (fullSaveDto != null)
                {
                        playerData = new PlayerData(fullSaveDto: fullSaveDto);
                        // Debug.Log("energyToCollect = " + playerData.energyToCollect);
                        onSetPlayerData.Invoke(playerData);
                }
        }

        public void ReloadHUD()
        {
                energyField.text = $"Energy: {playerData.energyAmount}";
                coinsField.text = $"Coins: {playerData.coinsAmount:0.0000}";
                stoneField.text = $"STONE: {playerData.currentResources[ResourceType.Stone]:0}";
                ironField.text = $"IRON: {playerData.currentResources[ResourceType.Iron]:0}";
                woodField.text = $"WOOD: {playerData.currentResources[ResourceType.Wood]:0}";
        }
}