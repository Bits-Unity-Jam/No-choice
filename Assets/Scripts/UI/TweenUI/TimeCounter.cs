using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.UI.Popups
{
    public class TimeCounter : BaseActivatingTween
    {
        [ SerializeField ]
        private TMP_Text tmpText;

        private int _currentSeconds;
        private int _currentMinutes;

        [ SerializeField ]
        private float transitionTime = 0.5f;

        public int TargetValue { get; set; }

        private int _targetSeconds;
        private int _targetMinutes;

        private void OnValidate() => tmpText ??= GetComponent<TextMeshProUGUI>();

        public void UpdateValue()
        {
            _finishedCounting = 0;
            
            DOTween.To(() => _currentMinutes, x => _currentMinutes = x, _targetMinutes, transitionTime)
                .OnUpdate(() => tmpText.text = $"{_currentMinutes}:{_currentSeconds}")
                .OnComplete(() => HandleCountingFinish()).SetUpdate(true);
            
            DOTween.To(() => _currentSeconds, y => _currentSeconds = y, _targetSeconds, transitionTime)
                .OnUpdate(() => tmpText.text = $"{_currentMinutes}:{_currentSeconds}")
                .OnComplete(() => HandleCountingFinish()).SetUpdate(true);
        }

        public override void DoActivate()
        {
            _targetSeconds = TargetValue % 60;
            _targetMinutes = TargetValue / 60;
            UpdateValue();
        }

        public override void DoDeactivate()
        {
            TargetValue = 0;
            UpdateValue();
        }

        public override void DoDeactivateImmediately()
        {
            tmpText.text = TargetValue.ToString();
        }

        private byte _finishedCounting = 0;
        private void HandleCountingFinish()
        {
            _finishedCounting++;
            if (_finishedCounting > 1)
            {
                SendTweenCompleted();
            }
        }
    }
}