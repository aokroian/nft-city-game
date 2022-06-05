using System;
using DG.Tweening;
using UnityEngine;

public class AnimatedVisuals : MonoBehaviour
{
    [SerializeField] private Transform visualsToPunch;
    public bool punchOnStart = true;
    public bool punchOnEnable = true;
    
    public float punchMultiplier = 1.1f;
    public float punchDuration = 0.1f;
    public int punchVibrato = 10;
    public float punchElasticity = 1f;

    public void PunchVisuals()
    {
        visualsToPunch.DOPunchScale(Vector3.one * punchMultiplier, punchDuration, punchVibrato, punchElasticity);
    }

    private void OnEnable()
    {
        if (punchOnEnable)
        {
            PunchVisuals();
        }
    }

    private void Start()
    {
        if (punchOnStart)
        {
            PunchVisuals();
        }
    }
}