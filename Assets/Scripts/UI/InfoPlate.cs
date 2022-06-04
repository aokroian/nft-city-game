using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class InfoPlate : MonoBehaviour
{
    #region Inspector

    public GameObject visiblePart;
    [Range(0.05f, 0.5f)] public float delayToShowVisiblePart = 0.2f;
    public Transform worldAnchor;
    public bool stayAtWorldAnchor;
    public float noResizeDistToCamera = 10;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI textField;

    #endregion

    #region Fields

    private Camera _camera;
    private float _startWorkingTime;

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        _camera = Camera.main;
        _startWorkingTime = Time.time;
        PositioningAndScaling();
    }

    private void OnEnable()
    {
        _startWorkingTime = Time.time;
    }

    private void OnDisable()
    {
        if (visiblePart.activeSelf) visiblePart.SetActive(false);
        _startWorkingTime = Time.time;
    }

    private void Update()
    {
        PositioningAndScaling();
        if (Mathf.Abs(Time.time - _startWorkingTime) >= delayToShowVisiblePart && !visiblePart.activeSelf)
        {
            visiblePart.SetActive(true);
        }
    }

    private void PositioningAndScaling()
    {
        if (!stayAtWorldAnchor) return;
        var targetWorldPos = worldAnchor.position;
        var distToCamera = Vector3.Distance(targetWorldPos, _camera.transform.position);
        var scale = noResizeDistToCamera / distToCamera;
        scale = Mathf.Clamp01(scale);
        Vector2 screenPos = _camera.WorldToScreenPoint(targetWorldPos);
        rectTransform.position = screenPos;
        rectTransform.localScale = new Vector3(scale, scale, scale);
    }

    #endregion
}