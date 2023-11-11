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

        public Action<float> EnergyChanged;
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
        }
    
        private IEnumerator UpdateTimer()
        {
            while (true)
            {
                ChangeEnergy(energyPerSecond, EnergyOperation.Subtract);
                yield return new WaitForSeconds(timeToTick);
            }
        }

        public void StartTimer()
        {
            StartCoroutine(UpdateTimer());
        }

        public void ChangeEnergy(float energyCount, EnergyOperation operation)
        {
            if (operation == EnergyOperation.Add)
            {
                if (_currentEnergy + energyCount > maxEnergy)
                {
                    _currentEnergy = maxEnergy;
                }
                else
                {
                    _currentEnergy += energyCount;
                }
            }
            else
            {
                if (_currentEnergy - energyCount < 0)
                {
                    _currentEnergy = 0;
                }
                else
                {
                    _currentEnergy -= energyCount;
                }
            }
            
            _currentPercentEnergy = _currentEnergy / maxEnergy;
            
            EnergyPercentChanged?.Invoke(_currentPercentEnergy, timeToTick);
        }
    }
}

