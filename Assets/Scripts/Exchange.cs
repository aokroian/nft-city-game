using UnityEngine;

public class Exchange : MonoBehaviour
{
        [SerializeField] private GameObject exchangeMenu;
        
        public void ToggleExchangeMenu(bool setActive)
        {
              exchangeMenu.SetActive(setActive);  
        }
        
        
}