using System;
using System.Threading;
using _Client.Scripts.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

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
