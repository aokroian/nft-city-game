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

        #region MonoBehaviour

        private void Start()
        {
            resource ??= GetComponentInParent<Resource>(); ;
        }

        private void OnEnable()
        {
            ReloadUI();
        }

        public void ReloadUI()
        {
            int energyLeft = 12346;
            int energyCost = 12345;
            var typeStr = resource.resourceType.ToString();
            resourceTypeField.text = typeStr;
            
            if (energyLeft >= energyCost)
            {
                mainField.text = $"Collect {typeStr} for {energyCost} energy?";
                collectButton.enabled = true;
            }
            else
            {
                mainField.text = $"You need {energyCost} energy to collect {typeStr}";
                collectButton.enabled = false;
            }
        }

        #endregion
    }
}