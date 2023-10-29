using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Energy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Shadowing
{
    public class ShadowingScreen : MonoBehaviour
    {
        private Image shadowBackground;
        private Tweener myTween;


        private void Awake()
        {
            shadowBackground = GetComponent<Image>();
        }

        private void Start()
        {
            EnergyController.Instance.EnergyPercentChanged += SetImageColor;
        }

        private void OnDestroy()
        {
            EnergyController.Instance.EnergyPercentChanged -= SetImageColor;
            if (myTween != null && myTween.IsActive())
            {
                myTween.Kill();
            }
        }

        private void SetImageColor(float value, float tickTIme)
        {
            if (shadowBackground == null) return;
            myTween = shadowBackground.DOFade(1-value, tickTIme).SetEase(Ease.Linear);
        }
    }
}

