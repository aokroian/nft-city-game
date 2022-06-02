using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UnityEvent<Interactable> onClick;

    private bool _disabledClicking = false;

    private void Awake()
    {
        onClick ??= new UnityEvent<Interactable>();
    }

    public void ToggleDisabledClicking(bool disabled)
    {
        _disabledClicking = disabled;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_disabledClicking)
        {
            onClick.Invoke(this);
        }
    }
}