using UnityEngine;
using DG.Tweening;

namespace Game.UI.Popups
{
    public class ActivatingShakeScaleTween : BaseActivatingTween
    { 
        [Space] 
        [Header("ShakeScale parameters")] 
        [SerializeField]
        private Vector3 shakeStrength = new(0.1f, 0.1f, 0.1f);
        [SerializeField]
        private float shakeDuration = 0.1f;
        
        public override void DoActivate() =>
            transform.DOShakeScale(shakeDuration, shakeStrength).SetUpdate(true).
                OnComplete(() => base.SendTweenCompleted());
        public override void DoDeactivate() =>
            transform.DOShakeScale(shakeDuration, shakeStrength).SetUpdate(true).
                OnComplete(() => base.SendTweenCompleted());

        public override void DoDeactivateImmediately(){}
    }
}