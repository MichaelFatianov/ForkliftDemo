using DG.Tweening;
using UnityEngine;

public class Hints : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _hideThreshold;
    private bool _isHidden;
    
    void Update()
    {
        if(_isHidden) return;
        if (!(Time.time > _hideThreshold)) return;
        _canvasGroup.DOFade(0f, 1f).SetEase(Ease.InCubic);
        _isHidden = true;
    }
}
