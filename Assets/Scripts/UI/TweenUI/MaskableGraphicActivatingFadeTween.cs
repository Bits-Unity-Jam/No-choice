using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Popups
{
    public class MaskableGraphicActivatingFadeTween : BaseActivatingTween
    {
        [SerializeField] 
        private List<MaskableGraphic> _graphics;
        
        [Space]
        [SerializeField] 
        private float activateDuration = 1;
        
        [SerializeField] 
        private float deactivateDuration = 0;

        [Space] 
        [Range(0, 1)]
        [SerializeField] 
        private float activatedFade = 1;
        
        [Range(0, 1)]
        [SerializeField] 
        private float deactivatedFade;
        
        [Space]
        [Header("Ease")]
        [SerializeField] 
        private Ease ease = Ease.OutBack;
        public override void DoActivate()
        {
            Sequence sequence = DOTween.Sequence();
            
            _graphics.ForEach(text => 
                sequence.Insert(0, text.DOFade(activatedFade, activateDuration).SetEase(ease)).SetUpdate(true));
            
            StopAllCoroutines();
            StartCoroutine(WaitForTweenCompletingRoutine(activateDuration));
        }

        public override void DoDeactivate()
        {
            Sequence sequence = DOTween.Sequence();
            
            _graphics.ForEach(text => 
                sequence.Insert(0, text.DOFade(deactivatedFade, deactivateDuration).SetEase(ease)).SetUpdate(true));
            
            StopAllCoroutines();
            StartCoroutine(WaitForTweenCompletingRoutine(deactivateDuration));
        }

        public override void DoDeactivateImmediately() => 
            _graphics.ForEach(text => text.DOFade(deactivatedFade, 0).SetUpdate(true));


        protected IEnumerator WaitForTweenCompletingRoutine(float time)
        {
            float start = Time.realtimeSinceStartup;
            
            while (Time.realtimeSinceStartup < start + time)
            {
                yield return null;
            }
            
            SendTweenCompleted();
        }
    }
}