using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Energy.UI
{
    public class SliderEnergy : MonoBehaviour
    {
        
        private Slider _sliderEnergy;

        private void Awake()
        {
            _sliderEnergy = GetComponent<Slider>();
        }

        private void Start()
        {
            _sliderEnergy.value = EnergyController.Instance.MaxEnergy;
            EnergyController.Instance.EnergyChanged += SetValueSlider;
        }

        private void OnDestroy()
        {
            EnergyController.Instance.EnergyChanged -= SetValueSlider;
        }

        private void SetValueSlider(float value, float timeToTick)
        {
            _sliderEnergy.DOValue(value, timeToTick).SetEase(Ease.Linear);
        }
    }
}