using Enums;
using MonoBehaviours;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        [Space(15)] [Header("PLAYER DATA DISPLAY")] [Space(5)]
        public PlayerInventory playerInventory;

        [Space(15)] [Header("HUD REFS")] [Space(5)] [SerializeField]
        private TextMeshProUGUI energyField;

        [SerializeField] private TextMeshProUGUI coinsField;
        [SerializeField] private TextMeshProUGUI woodField;
        [SerializeField] private TextMeshProUGUI stoneField;
        [SerializeField] private TextMeshProUGUI ironField;


        [Space(15)] [Header("EVENTS")] [Space(5)]
        public UnityEvent<PlayerInventory> onSetPlayerData;

        private void Awake()
        {
            onSetPlayerData ??= new UnityEvent<PlayerInventory>();
        }

        public void SetPlayerData(FullSaveDto fullSaveDto)
        {
            if (fullSaveDto == null) return;
            playerInventory.UpdateInventory(fullSaveDto);
            onSetPlayerData.Invoke(playerInventory);
        }

        public void ReloadHUD()
        {
            energyField.text = $"Energy: {playerInventory.Energy}";
            coinsField.text = $"Coins: {playerInventory.Coins:0.0000}";
            // stoneField.text = $"STONE: {playerInventory.Resources[ResourceType.Stone]:0}";
            // ironField.text = $"IRON: {playerInventory.Resources[ResourceType.Iron]:0}";
            // woodField.text = $"WOOD: {playerInventory.Resources[ResourceType.Wood]:0}";
        }
    }
}