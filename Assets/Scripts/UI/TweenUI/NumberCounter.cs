using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.UI.Popups
{
    public class NumberCounter : BaseActivatingTween
    {
        [SerializeField]
        private TMP_Text tmpText;
        
        private int _currentValue;
        [SerializeField]
        private float transitionTime = 1;
        [SerializeField] private bool needToUpscale;
        [SerializeField] private Transform transformToUpscale;
        [SerializeField] private Vector3 toScale;

        [ SerializeField ]
        private string prefix;
        public int TargetValue { get; set; }
        
        private void OnValidate() => tmpText ??= GetComponent<TextMeshProUGUI>();

        private void UpdateValue()
        {
            if (needToUpscale)
            {
                transformToUpscale.DOScale(toScale, 0.1f);
            }
            DOTween.To(() => _currentValue, x => _currentValue = x, TargetValue, transitionTime)
                .OnUpdate(() => tmpText.text = $"{prefix}{_currentValue.ToString()}").OnComplete(() => base.SendTweenCompleted())
                .SetUpdate(true).OnComplete(() =>  transformToUpscale.DOScale(Vector3.one, 0.075f));
        }

        public override void DoActivate() => UpdateValue();

        public override void DoDeactivate()
        {
            TargetValue = 0;
            UpdateValue();
        }

        public override void DoDeactivateImmediately()
        {
            _currentValue = 0;
            tmpText.text = _currentValue.ToString();
        }
    }
}