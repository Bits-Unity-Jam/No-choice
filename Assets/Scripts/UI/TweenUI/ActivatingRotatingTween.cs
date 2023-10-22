using DG.Tweening;
using UnityEngine;

namespace Game.UI.Popups
{
    public class ActivatingRotatingTween : BaseActivatingTween
    {
        [SerializeField] 
        private Vector3 endRotation = new(0, 0, -30);
        
        [SerializeField] 
        private Vector3 startRotation = new(0, 0, 30);

        [SerializeField] 
        private float rotationDuration;
        
        [SerializeField] 
        private bool isLooped;

        [SerializeField] 
        private Ease ease = Ease.Linear;

        private int _loops;

        private Sequence _sequence;
        
        public override void DoActivate()
        {
            _loops = isLooped ? -1 : 1;

            base.SendTweenCompleted();
            
            _sequence = DOTween.Sequence();

            _sequence.Append(transform.DORotate(endRotation, rotationDuration).SetEase(ease).SetUpdate(true));
            _sequence.Append(transform.DORotate(startRotation, rotationDuration).SetEase(ease).SetUpdate(true));
            _sequence.SetLoops(_loops).SetUpdate(true);
        }

        public override void DoDeactivate()
        {
            _sequence.Kill();
            //transform.DORotate(startRotation, rotationDuration).SetEase(ease).SetUpdate(true);
        }

        public override void DoDeactivateImmediately() => transform.rotation = Quaternion.Euler(startRotation);
    }
}