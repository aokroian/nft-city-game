using MonoBehaviours;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourceUI : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private Resource resource;
        [SerializeField] private TextMeshProUGUI resourceTypeField;
        [SerializeField] private TextMeshProUGUI mainField;

        [SerializeField] private Button collectButton;
        //[SerializeField] private Button closeButton;

        #endregion

        #region Fields

        private HUD _hud;

        #endregion


        #region MonoBehaviour

        private void Start()
        {
            resource ??= GetComponentInParent<Resource>();
            
        }

        private void OnEnable()
        {
            _hud ??= GameObject.Find("Player").GetComponent<HUD>();
            ReloadUI();
        }

        public void ReloadUI()
        {
            
            int energyLeft = _hud.playerInventory.Energy;
            int energyCost = resource.itemSettings.collectEnergyCost; 
            var typeStr = resource.itemSettings.resourceType.ToString();
            resourceTypeField.text = typeStr;
            
            if (energyLeft >= energyCost)
            {
                mainField.text = $"Collect {typeStr} for {energyCost} energy?";
                collectButton.gameObject.SetActive(true);
            }
            else
            {
                mainField.text = $"You need {energyCost} energy to collect {typeStr}";
                collectButton.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}