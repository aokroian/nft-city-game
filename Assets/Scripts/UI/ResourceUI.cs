using DataClasses;
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

        private Player player;

        #endregion


        #region MonoBehaviour

        private void Start()
        {
            resource ??= GetComponentInParent<Resource>();
            
        }

        private void OnEnable()
        {
            player ??= GameObject.Find("Player").GetComponent<Player>();
            ReloadUI();
        }

        public void ReloadUI()
        {
            
            int energyLeft = player.playerData.energyAmount;
            int energyCost = player.playerData.energyToCollect;
            var typeStr = resource.resourceType.ToString();
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