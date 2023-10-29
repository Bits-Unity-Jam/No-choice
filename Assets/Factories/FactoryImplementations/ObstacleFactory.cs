using System;
using Assets.Scripts.Pools.BasePools;
using Chunks;
using UnityEngine;
using Zenject;

namespace Factories.FactoryImplementations
{
    public class ObstacleFactory : MonoBehaviour, IFactory<ObstacleId, Obstacle>
    {
        [SerializeField] private ObstaclePoolingService _poolingService;


        public Obstacle Create(ObstacleId obstacleId)
        {
            ObjectPool obstaclePool = _poolingService.GetObstaclePool(obstacleId);

            return obstaclePool.PullObject().GetComponent<Obstacle>();
        }
    }

    [Serializable]
    public struct ObstaclePoolIdCompliance
    {
        [SerializeField] private ObjectPool _obstaclePool;

        [SerializeField] private ObstacleId _id;

        public ObjectPool ObstaclePool
        {
            get => _obstaclePool;
            set => _obstaclePool = value;
        }

        public ObstacleId ID
        {
            get => _id;
            set => _id = value;
        }
    }
}