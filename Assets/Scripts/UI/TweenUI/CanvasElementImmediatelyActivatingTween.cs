using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Popups
{
    public class CanvasElementImmediatelyActivatingTween : BaseActivatingTween
    {
        [Header("Activation/Deactivation")] 
        [SerializeField]
        private List<BaseActivatingTween> baseActivationTweeners;

        private int _activatingTweenersCount;
        private int _completedTweenerCount;

        public override void DoActivate()
        {
            if (baseActivationTweeners is null) return;
            
            _activatingTweenersCount = baseActivationTweeners.Count;

            SubscribeToTweenerCompleted();
            
            baseActivationTweeners.ForEach(tweener => tweener.DoActivate());
        }

        public void AddTweener(BaseActivatingTween tween) => baseActivationTweeners.Add(tween);
        
        private void SubscribeToTweenerCompleted() => 
            baseActivationTweeners.ForEach(tweener => tweener.OnTweenCompleted += HandleTweenComplete);

        private void HandleTweenComplete(BaseActivatingTween completedTween)
        {
            completedTween.OnTweenCompleted -= HandleTweenComplete;
            
            if (_completedTweenerCount == _activatingTweenersCount - 1)
            {
                Debug.Log($"[CanvasElementActivatingTween] on {gameObject.name} completed!");
                base.SendTweenCompleted();
            }
            
            _completedTweenerCount++;
        }
        
        public override void DoDeactivate() => 
            baseActivationTweeners.ForEach(tweener => tweener.DoDeactivate());

        public override void DoDeactivateImmediately() => 
            baseActivationTweeners.ForEach(tweener => tweener.DoDeactivateImmediately());

        public void ClearTweeners() => baseActivationTweeners.Clear();
    }
}