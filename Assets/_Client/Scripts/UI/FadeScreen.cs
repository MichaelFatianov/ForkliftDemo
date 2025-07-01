using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class FadeScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        public void Fade(float duration, FadeType fadeType, Action onComplete = null)
        {
            switch (fadeType)
            {
                case FadeType.FadeIn:
                    canvasGroup.DOFade(1f, duration).SetEase(Ease.InQuad).OnComplete(() => onComplete?.Invoke());
                    break;
                case FadeType.FadeOut:
                    canvasGroup.DOFade(0f, duration).SetEase(Ease.InQuad).OnComplete(() => onComplete?.Invoke());
                    break;
            }
        }
    }
}