using UnityEngine;
using Zenject;

namespace Chunks.ChunkRedactor
{
    public class ObstacleCreator : MonoBehaviour
    {
        private IFactory<ObstacleId, Obstacle> _obstacleFactory;
        
        [Inject]
        private void Construct(IFactory<ObstacleId, Obstacle> obstacleFactory)
        {
            _obstacleFactory = obstacleFactory;
        }
        
        public Obstacle Create(ObstacleId obstacleId)
        {
            return _obstacleFactory.Create(obstacleId);
        }
    }
}
