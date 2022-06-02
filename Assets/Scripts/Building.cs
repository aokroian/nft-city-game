using System;
using UnityEngine;

public class Building: Interactable
{
        #region Inspector

        public GameObject buildingMenu;

        #endregion
        
        #region Fields

        public bool isMenuOpen = false;

        #endregion


        #region MonoBehaviour

        private void Start()
        {
        }

        public void ToggleBuildingMenu()
        {
                buildingMenu.SetActive(!isMenuOpen);
                isMenuOpen = !isMenuOpen;
        }

        #endregion
}