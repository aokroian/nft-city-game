using System;
using UnityEngine;
using DG.Tweening;

namespace UI
{
    public class AnimatedUI : MonoBehaviour
    {
        public bool expandOnEnable = true;
        [Range(0.01f, 1f)]public float expandTime = 0.5f;
        public float punchMultiplier = 1.1f;
        public float punchDuration = 0.1f;
        public int punchVibrato = 10;
        public float punchElasticity = 1f;

        [SerializeField] private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform ??= GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            if (expandOnEnable)
            {
                rectTransform.localScale = Vector3.zero;
                rectTransform.DOScale(Vector3.one, expandTime).OnComplete(() =>
                {
                    rectTransform.DOPunchScale(Vector3.one * punchMultiplier, punchDuration, punchVibrato, punchElasticity).OnComplete(
                        () =>
                        {
                            rectTransform.DOScale(Vector3.one, 0.2f);
                        });
                });

            }
            
        }
    }
}