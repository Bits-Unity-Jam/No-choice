using System.Collections;
using System.Collections.Generic;
using BasePools;
using UnityEngine;

namespace Assets.Scripts.Pools.BasePools
{
    public class PoolObject : MonoBehaviour
    {
        private ObjectPool _objectPool;
        public void Initialize(ObjectPool objectPool)
        {
            _objectPool = objectPool;
        }
        public virtual void PushToPool()
        {
            gameObject.SetActive(false);
            if (_objectPool != null)
            {
                _objectPool.PushObject(this);
            }
            else
            {
                Destroy(gameObject);
            }

        }
        public virtual PoolObject Pull()
        {
            return this;
        }
        public ObjectPool GetPool()
        {
            return _objectPool;
        }
    }

}
