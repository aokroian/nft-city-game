using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public InteractableEvents pointerEvents;

    private bool _disabledClicking = false;
    private bool _isHovered = false;

    private void Awake()
    {
        pointerEvents.onClick ??= new UnityEvent<Interactable>();
        pointerEvents.onPointerEnter ??= new UnityEvent<Interactable>();
        pointerEvents.onPointerExit ??= new UnityEvent<Interactable>();
        _isHovered = false;
    }

    protected void OnEnable()
    {
        _isHovered = false;
    }

    protected void OnDisable()
    {
        _isHovered = false;
    }

    protected bool IsHovered()
    {
        return _isHovered;
    }

    public void ToggleDisabledClicking(bool disabled)
    {
        _disabledClicking = disabled;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEvents.onPointerEnter.Invoke(this);
        _isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerEvents.onPointerExit.Invoke(this);
        _isHovered = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_disabledClicking)
        {
            pointerEvents.onClick.Invoke(this);
        }
    }
}

[Serializable]
public class InteractableEvents
{
    public UnityEvent<Interactable> onClick;
    public UnityEvent<Interactable> onPointerEnter;
    public UnityEvent<Interactable> onPointerExit;
}