using Assets.Scripts.Pools.BasePools;
using UnityEngine;

namespace Pools.Containers
{
    public abstract class BaseContainer : MonoBehaviour
    {
        public abstract PoolObject PullObject();
        public abstract void PushObject(PoolObject obj);
        public abstract int GetLength();
    }

}