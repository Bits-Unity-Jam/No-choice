using System;
using UnityEngine;
using Zenject;

namespace Chunks.ChunkRedactor
{
    public class ObstacleCreator : MonoBehaviour
    {
        [SerializeField] private Transform _createdObjectsParentTransform;
        
        private IFactory<ObstacleId, Obstacle> _obstacleFactory;
        
        [Inject]
        private void Construct(IFactory<ObstacleId, Obstacle> obstacleFactory)
        {
            _obstacleFactory = obstacleFactory;
        }
        
        public Obstacle Create(ObstacleId obstacleId)
        {
            Obstacle created = _obstacleFactory.Create(obstacleId);
            created.ReturnToDefaultState();
            created.transform.parent = _createdObjectsParentTransform;
            created.transform.localPosition = Vector3.zero;
            created.name = Enum.GetNames(typeof(ObstacleId))[(int)obstacleId];

            return created;
        }
    }
}
