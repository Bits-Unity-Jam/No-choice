using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Energy.Booster
{
    public class EnergyBooster : MonoBehaviour
    {
        [SerializeField]
        private float countEnergy;

        private bool isActivate = false;
        private const string PLAYER = "Factory";
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(PLAYER))
            {
                if (!isActivate)
                {
                    isActivate = true;
                    EnergyController.Instance.ChangeEnergy(countEnergy, EnergyOperation.Add);
                    
                    StartCoroutine(DestroyObject());
                }
                
            }
        }
        
        private IEnumerator DestroyObject()
        {
            yield return new WaitForSeconds(0f);
            Destroy(this.gameObject); 
        }
    }
}

