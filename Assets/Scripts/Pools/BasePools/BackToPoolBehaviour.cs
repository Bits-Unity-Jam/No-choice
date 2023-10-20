using System.Collections;
using Assets.Scripts.Pools.BasePools;
using UnityEngine;

namespace BasePools
{
    public class BackToPoolBehaviour : MonoBehaviour
    {
        private PoolObject _poolObject;
        
        private void OnValidate()
        {
            _poolObject = GetComponent<PoolObject>();
        }

        private void Awake()
        {
            _poolObject ??= GetComponent<PoolObject>();
        }

        public void BackToParent(float seconds = 3.5f,  bool isActiveAfterBack = false)
        {
            if (seconds <= 0)
            {
                Back(isActiveAfterBack);
                return;
            }
            
            StartCoroutine(Back(seconds, isActiveAfterBack));
        }
        private IEnumerator Back(float seconds, bool isActiveAfterBack)
        {
            yield return new WaitForSeconds(seconds);
            Back(isActiveAfterBack);
        }

        private void Back(bool isActiveAfterBack)
        {
            _poolObject.PushToPool();
            gameObject.SetActive(isActiveAfterBack);
        }
    }
}