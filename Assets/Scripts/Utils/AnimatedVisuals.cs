using DG.Tweening;
using UnityEngine;

namespace Utils
{
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
            visualsToPunch.DOPunchScale(new Vector3(0.07f, punchMultiplier, 0.07f), punchDuration, punchVibrato, punchElasticity).OnComplete(
                () =>
                {
                    visualsToPunch.DOScale(Vector3.one, 0.2f);
                });
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
}