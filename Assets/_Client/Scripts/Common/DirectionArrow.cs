using DG.Tweening;
using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    void Start()
    {
        transform.DOLocalMoveY(transform.localPosition.y + 3f, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
