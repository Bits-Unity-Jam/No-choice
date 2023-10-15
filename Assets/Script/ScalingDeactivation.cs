using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScalingDeactivation : MonoBehaviour
{
    [SerializeField] private UnityEvent onAnimationCompleted;

    [SerializeField] private float duration =1.0f;
    [SerializeField] private Transform transformToScale;
    public void Scale()
    {
        transformToScale ??= transform.parent;
        transformToScale.DOScale(Vector2.zero, duration).OnComplete(() => { onAnimationCompleted?.Invoke(); });
    }
}
