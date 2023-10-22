using System.Collections.Generic;
using Assets.Scripts.Pools.BasePools;

namespace Pools.Containers.QueueContainer
{
    public class PoolQueueContainer : BaseContainer
    {
        private Queue<PoolObject> _poolQueue = new();

        public override PoolObject PullObject()
        {
            return _poolQueue.Dequeue();
        }

        public override void PushObject(PoolObject obj)
        {
            _poolQueue.Enqueue(obj);
        }

        public override int GetLength()
        {
            return _poolQueue.Count;
        }
    }
}