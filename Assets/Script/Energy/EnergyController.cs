using System;
using System.Collections;
using UnityEngine;

namespace Game.Energy
{
    public class EnergyController : MonoBehaviour
    {
        [Header("Energy Setting")]
        [SerializeField]
        private float maxEnergy = 100;
        [SerializeField]
        private float energyPerSecond = 1;
        [Range(0.0f, 5f)]
        [SerializeField]
        private float timeToTick = 1;

        private float _currentEnergy;
        private float _currentPercentEnergy;
        public static EnergyController Instance;
        
        public float MaxEnergy => maxEnergy;
        public float CurrentEnergy => _currentEnergy;
        public float CurrentPercentEnergy => _currentPercentEnergy;

        public Action<float, float> EnergyChanged;
        public Action<float, float> EnergyPercentChanged;
        
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            _currentEnergy = maxEnergy;
            StartCoroutine(UpdateTimer());
        }
    
        private IEnumerator UpdateTimer()
        {
            while (true)
            {
                ChangeEnergy(energyPerSecond, EnergyOperation.Subtract);
                yield return new WaitForSeconds(timeToTick);
            }
        }

        public void ChangeEnergy(float energyCount, EnergyOperation operation)
        {
            if (operation == EnergyOperation.Add)
            {
                _currentEnergy += energyCount;
            }
            else
            {
                _currentEnergy -= energyCount;
            }

            _currentPercentEnergy = ((CurrentEnergy / MaxEnergy) * 100) / 100;
           
            EnergyPercentChanged?.Invoke(_currentPercentEnergy, timeToTick);
            EnergyChanged?.Invoke(_currentEnergy, timeToTick);
        }
    }
}

