using System;
using UnityEngine;
using UnityEngine.Events;

public class Building : Interactable
{
    #region Inspector

    [Space(15)] [Header("REFS")] [Space(5)]
    public GameObject buildingMenu;

    public GameObject infoPlate;

    [Space(15)] [Header("EVENTS")] [Space(5)]
    public UnityEvent onDisable;

    public UnityEvent onEnable;

    #endregion

    #region Fields

    public bool isMenuOpen = false;

    #endregion


    #region MonoBehaviour

    private void Start()
    {
        onDisable ??= new UnityEvent();
        onEnable ??= new UnityEvent();
    }

    private new void  OnEnable()
    {
        base.OnEnable();
        onEnable.Invoke();
    }

    private new void OnDisable()
    {
        base.OnDisable();
        onDisable.Invoke();
    }

    public void ToggleBuildingMenu()
    {
        buildingMenu.SetActive(!isMenuOpen);
        isMenuOpen = !isMenuOpen;
    }

    public void CheckIsHovered()
    {
        infoPlate.SetActive(IsHovered());
    }

    #endregion
}