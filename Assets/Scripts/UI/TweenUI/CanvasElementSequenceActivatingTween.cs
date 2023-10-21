using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.UI.Popups
{
    public class CanvasElementSequenceActivatingTween : BaseActivatingTween
    {
        [Header("Activation/Deactivation")]
        [SerializeField]
        private List<BaseActivatingTween> baseActivationTweeners;

        private int _index = 0;

        public List<BaseActivatingTween> BaseActivationTweeners => baseActivationTweeners;
        
        public override void DoActivate()
        {
            if (baseActivationTweeners.FirstOrDefault() == default)
            {
                SendTweenCompleted();
                return;
            }
            
            _index = 0;
            
            baseActivationTweeners.First().OnTweenCompleted += HandleTweenCompleted;
            baseActivationTweeners.First().DoActivate();
        }

        private void HandleTweenCompleted(BaseActivatingTween completedTween)
        {
            baseActivationTweeners[_index].OnTweenCompleted -= HandleTweenCompleted;
            
            _index++;
            
            if (_index >= baseActivationTweeners.Count)
            {
                SendTweenCompleted();
                return;
            }
            
            baseActivationTweeners[_index].OnTweenCompleted += HandleTweenCompleted;
            baseActivationTweeners[_index].DoActivate(); 
        }

        public override void DoDeactivate() => 
            baseActivationTweeners.ForEach(tweener => tweener.DoDeactivate());
 
        public override void DoDeactivateImmediately() => 
            baseActivationTweeners.ForEach(tweener => tweener.DoDeactivateImmediately());

    }
}