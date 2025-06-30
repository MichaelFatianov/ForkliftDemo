using System;
using System.Threading;
using _Client.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private AnimationCurve fadeCurve;
    private CancellationTokenSource _cts;

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    public async UniTask Fade(float duration, FadeType fadeType, Action onComplete = null)
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;
        var time = 0f;
        try
        {
            while (time < duration)
            {
                token.ThrowIfCancellationRequested();
                var t = time / duration;
                switch (fadeType)
                {
                    case FadeType.FadeIn:
                        canvasGroup.alpha = fadeCurve.Evaluate(t);
                        break;
                    case FadeType.FadeOut:
                        canvasGroup.alpha = fadeCurve.Evaluate(1 - t);
                        break;
                }

                time += Time.deltaTime;
                
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }

            onComplete?.Invoke();
        }
        catch (OperationCanceledException)
        {
            Debug.Log("FadeScreen: OperationCanceledException");
        }
    }
}
