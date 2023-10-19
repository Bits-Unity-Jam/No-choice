using System.Collections;
using System.Collections.Generic;
using BasePools;
using UnityEngine;
using Mechanics;
using Mechanics.Pools;
using Pools.BasePools;

namespace Mechanics.Pools
{
    public class PoolObject : MonoBehaviour
    {
        private ObjectPool _objectPool;
        public void Initialize(ObjectPool objectPool)
        {
            this._objectPool = objectPool;
        }
        public virtual void PushToPool()
        {
            gameObject.SetActive(false);
            if(_objectPool != null)
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
