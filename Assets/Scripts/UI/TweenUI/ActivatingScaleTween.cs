using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Game.UI.Popups
{
    public class ActivatingScaleTween : BaseActivatingTween
    {
        [ Header("Activate/Deactivate parameters") ]
        [ SerializeField ]
        private Vector3 activatedScale;

        [ SerializeField ]
        private Vector3 deactivatedScale;

        [ SerializeField ]
        private float activatedScaleDuration = 1;

        [ SerializeField ]
        private float deactivatedScaleDuration;

        [ Space ]
        [ Header("Ease") ]
        [ SerializeField ]
        private Ease ease = Ease.OutBack;

        private TweenerCore<Vector3, Vector3, VectorOptions> _previousTweener;

        public override void DoActivate()
        {
            _previousTweener.Kill();
            _previousTweener = transform.DOScale(activatedScale, activatedScaleDuration).SetEase(ease).SetUpdate(true).OnComplete(() =>
                base.SendTweenCompleted());
        }

        public override void DoDeactivate()
        {
            _previousTweener.Kill();
            _previousTweener = transform.DOScale(deactivatedScale, deactivatedScaleDuration).SetEase(ease).SetUpdate(true).OnComplete(() =>
                base.SendTweenCompleted());
        }

        public override void DoDeactivateImmediately() => transform.DOScale(deactivatedScale, 0).SetUpdate(true);
    }
}