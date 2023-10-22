using Assets.Script.Chunks;
using Chunks;
using UnityEngine;
using Zenject;

namespace Factories.FactoryImplementations
{
    public class ObstacleFactory : IFactory<ObstacleId, Obstacle>
    {
        public Obstacle Create(ObstacleId param)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ObstaclePoolingService : MonoBehaviour
    {
        
    }
}
