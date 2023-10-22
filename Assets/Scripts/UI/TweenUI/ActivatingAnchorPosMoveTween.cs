using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Game.UI.Popups
{
    public class ActivatingAnchorPosMoveTween : BaseActivatingTween
    {
        [SerializeField] 
        private RectTransform objectTransform;
        
        
        [Space]
        [Header("Positions")]
        [SerializeField] 
        private Vector2 activatedPosition;
        [SerializeField] 
        private Vector2 deactivatedPosition;
        
        [Space]
        [Header("Durations")]
        [SerializeField] 
        private float activatedDuration = 1;
        [SerializeField] 
        private float deactivatedDuration = 0;
        
        [Space]
        [Header("Ease")]
        [SerializeField] 
        private Ease ease = Ease.OutBack;
        private void OnValidate() => objectTransform ??= GetComponent<RectTransform>();

        public override void DoActivate() =>
            objectTransform.DOAnchorPos(activatedPosition, activatedDuration).SetUpdate(true).SetEase(ease).OnComplete(() => 
                base.SendTweenCompleted());

        public override void DoDeactivate() =>
            objectTransform.DOAnchorPos(deactivatedPosition, deactivatedDuration).SetUpdate(true).SetEase(ease).OnComplete(() => 
                base.SendTweenCompleted());

        public override void DoDeactivateImmediately() => 
            objectTransform.DOAnchorPos(deactivatedPosition, 0).SetUpdate(true);
        
    }
}