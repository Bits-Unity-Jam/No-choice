using System;
using System.Collections.Generic;
using Assets.Scripts.Pools.BasePools;
using UnityEngine;
using Zenject;

namespace Chunks.ChunkRedactor
{
    [RequireComponent(typeof(ChunkKeeper))]
    public class ObstacleCreator : MonoBehaviour
    {
        [SerializeField] private Transform _createdObjectsParentTransform;
        
        [SerializeField] private List<Obstacle> _createdObstacles;
        
        private IFactory<ObstacleId, Obstacle> _obstacleFactory;

        [Inject]
        private void Construct(IFactory<ObstacleId, Obstacle> obstacleFactory)
        {
            _obstacleFactory = obstacleFactory;
        }

        public Obstacle Create(ObstacleId obstacleId)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                Debug.LogError("The application isn't running! Please start the game before trying to create objects!");
                return default;
            }
#endif        
            Obstacle created = _obstacleFactory.Create(obstacleId);
            created.ReturnToDefaultState();
            created.transform.parent = _createdObjectsParentTransform;
            created.transform.localPosition = Vector3.zero;
            created.name = Enum.GetNames(typeof(ObstacleId))[(int)obstacleId];
            _createdObstacles.Add(created);
            return created;
        }

        public Obstacle Create(ObstacleData obstacleData)
        {
            Obstacle created = Create(obstacleData.ObstacleId);

            created.ApplyData(obstacleData);
            
            return created;
        }
        
        public void Clear()
        {
            _createdObstacles.ForEach(obstacle => obstacle.GetComponent<PoolObject>().PushToPool());
            _createdObstacles.Clear();
        }
    }
}