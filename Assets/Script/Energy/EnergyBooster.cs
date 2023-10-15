using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Energy.Booster
{
    public class EnergyBooster : MonoBehaviour
    {
        [SerializeField]
        private float countEnergy;
        [SerializeField]
        private AudioSource audioSource;

        private bool isActivate = false;
        private const string PLAYER = "Factory";

        [SerializeField]
        private UnityEvent onDestroy;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(PLAYER))
            {
                if (!isActivate)
                {
                    isActivate = true;
                    EnergyController.Instance.ChangeEnergy(countEnergy, EnergyOperation.Add);

                    audioSource.Play();
                    
                    onDestroy.Invoke();
                }
                
            }
        }

    }
}

