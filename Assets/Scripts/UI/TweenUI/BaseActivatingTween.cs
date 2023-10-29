using System;
using UnityEngine;

namespace Game.UI.Popups
{
    public abstract class BaseActivatingTween : MonoBehaviour
    {
        public event Action<BaseActivatingTween> OnTweenCompleted;
        
        public abstract void DoActivate();
        
        public abstract void DoDeactivate();
        public abstract void DoDeactivateImmediately();

        protected void SendTweenCompleted() => OnTweenCompleted?.Invoke(this);
        
    }
}