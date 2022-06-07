using System;
using DG.Tweening;
using Enums;
using Events;
using TMPro;
using UI;
using UnityEngine;

public class Resource : Interactable
{
    public ResourceType resourceType;
    public int resourceAmount;
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
        apiProvider.concreteApi.CollectResource(resourceType, CollectingResult, err =>
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
        infoPlateText.text = $"{resourceType}";
        ReloadVisuals();
    }

    private void ReloadVisuals()
    {
        foreach (Transform t in visualsContainer)
        {
            t.gameObject.SetActive(t.name == $"{resourceType}");
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