using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Pools.BasePools;
using Assets.Scripts.Pools.BasePools.Sources;
using UnityEngine;

namespace Factories.FactoryImplementations
{
    public class ObstaclePoolingService : MonoBehaviour
    {
        [SerializeField] private GameObject _obstaclePoolPrefab;
        [SerializeField] private Transform _poolParentTransform;
        

        [SerializeField] private List<ObstaclePoolIdCompliance> _obstaclePoolIdCompliances;

        
        private void OnValidate()
        {
            UpdatePools();
        }

        private void Awake()
        {
            UpdatePools();
        }

        private void UpdatePools()
        {
            int obstacleIdsCount = Enum.GetNames(typeof(ObstacleId)).Length;

            if (_obstaclePoolIdCompliances.Count == obstacleIdsCount) return;

            foreach (Transform t in _poolParentTransform)
            {
                Destroy(t);
            }

            _obstaclePoolIdCompliances.Clear();

            for (int i = 0; i < obstacleIdsCount; i++)
            {
                _obstaclePoolIdCompliances.Add(new ObstaclePoolIdCompliance
                {
                    ObstaclePool = Instantiate(_obstaclePoolPrefab, _poolParentTransform).GetComponent<ObjectPool>(),
                    ID = (ObstacleId)i
                });
            }

            for (var index = 0; index < _obstaclePoolIdCompliances.Count; index++)
            {
                var obstaclePoolIdCompliance = _obstaclePoolIdCompliances[index];
                obstaclePoolIdCompliance.ObstaclePool.GetComponent<ObstacleObjectSource>().ObstacleId = (ObstacleId)index;
            }
        }

        public ObjectPool GetObstaclePool(ObstacleId obstacleId)
        {
            return _obstaclePoolIdCompliances.FirstOrDefault(compliance => compliance.ID == obstacleId).ObstaclePool;
        }
    }
}