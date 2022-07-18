using DataClasses;
using DG.Tweening;
using MonoBehaviours;
using TMPro;
using UI;
using UnityEngine;

namespace MonoBehaviours
{
    public class Resource : Interactable
    {
        public ResourceItem itemSettings;
        public ServerApiProvider apiProvider;

        [SerializeField] private Transform visualsContainer;
        [SerializeField] private InfoPlate infoPlate;
        [SerializeField] private TextMeshProUGUI infoPlateText;

        [SerializeField] private GameObject resourceMenu;

        #region MonoBehaviour

        #region Fields

        public bool isMenuOpen = false;

        #endregion

        public void CollectResource()
        {
            Debug.Log("collecting");
            apiProvider.concreteApi.CollectResource(itemSettings, CollectingResult, err =>
            {
                Debug.Log("Collecting resource error");
                // TODO: Err!
            });
        }

        public void CollectingResult(bool result)
        {
            if (result)
            {
                resourceMenu.SetActive(false);
                infoPlate.Collected();
                visualsContainer.GetComponentInChildren<MeshRenderer>(false).material.DOFade(0f, 1f);
                Destroy(gameObject, 1f);
            }
            else
            {
                Debug.Log("Can't collect");
                // TODO: Message "can't collect"
            }
        }

        public void Start()
        {
            infoPlateText.text = $"{itemSettings.resourceType}";
            ReloadVisuals();
        }

        private void ReloadVisuals()
        {
            foreach (Transform t in visualsContainer)
            {
                t.gameObject.SetActive(t.name == $"{itemSettings.resourceType}");
            }
        }

        public void ToggleMenuOpen()
        {
            resourceMenu.SetActive(!isMenuOpen);
            isMenuOpen = !isMenuOpen;
            enabled = !isMenuOpen;
            if (isMenuOpen)
            {
                GetComponentInChildren<ResourceUI>().ReloadUI();
            }
        }

        #endregion

    }
}