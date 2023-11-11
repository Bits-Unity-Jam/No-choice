using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Energy.UI
{
    public class SliderEnergy : MonoBehaviour
    {
        [Header("Sprite")]
        [SerializeField]
        private List<Sprite> left;
        [SerializeField]
        private List<Sprite> center;
        [SerializeField]
        private List<Sprite> right;

        [Header("Elements Slider")]
        [SerializeField]
        private List<Image> elementSlider;

        private float _timeToTick = 0;
        private const int RED = 0, ORANGE = 1, YELLOW = 2, GREEN = 3;

        private void Start()
        {
            EnergyController.Instance.EnergyPercentChanged += SetValueSlider;
        }

        private void OnDestroy()
        {
            EnergyController.Instance.EnergyPercentChanged -= SetValueSlider;
        }

        private void SetValueSlider(float percent, float timeToTick)
        {
            CheckImageSlider(percent);
            CheckPercentImage(percent);

            _timeToTick = timeToTick;
        }


        #region Percent Energy

        private void CheckPercentImage(float percent)
        {
            int stepCount = (int)Math.Round(percent / 0.1, MidpointRounding.AwayFromZero);

            ChangePercentImage(stepCount);
        }

        private void ChangePercentImage(int count)
        {
            int targetCount = Math.Min(count, elementSlider.Count);

            for (int i = 0; i < elementSlider.Count; i++)
            {
                if (i < targetCount)
                {
                    if (elementSlider[i].color.a < 1f)
                    {
                        elementSlider[i].DOFade(1f, _timeToTick);
                    }
                }
                else
                {
                    if (elementSlider[i].color.a > 0f)
                    {
                        elementSlider[i].DOFade(0f, _timeToTick);
                    }
                }
            }
        }

        #endregion

        #region Color Energy

        private void CheckImageSlider(float percent)
        {
            int id = 0;
            int stepCount = (int)Math.Floor(percent / 0.25);

            switch (stepCount)
            {
                case 0:
                    id = RED;
                    break;
                case 1:
                    id = ORANGE;
                    break;
                case 2:
                    id = YELLOW;
                    break;
                case 3:
                    id = GREEN;
                    break;
                case 4:
                    id = GREEN;
                    break;
                default:
                    break;
            }

            ChangeImageSlider(id);
        }
        
        private void ChangeImageSlider(int id)
        {
            if (id < 0 || id >= center.Count)
            {
                return;
            }

            elementSlider[0].sprite = left[id];

            for (int i = 1; i < elementSlider.Count - 1; i++)
            {
                elementSlider[i].sprite = center[id];
            }
        }

        #endregion

    }
}