using System;
using DataClasses;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
        [Space(15)] [Header("PLAYER DATA DISPLAY")] [Space(5)]
        public PlayerData playerData;

        public UnityEvent<PlayerData> onSetPlayerData;

        private void Awake()
        {
                onSetPlayerData ??= new UnityEvent<PlayerData>();
                playerData ??= new PlayerData();
        }

        public void SetPlayerData(FullSaveDto fullSaveDto)
        {
                if (fullSaveDto != null)
                {
                        playerData = new PlayerData(fullSaveDto: fullSaveDto);
                        onSetPlayerData.Invoke(playerData);
                }
        }
}