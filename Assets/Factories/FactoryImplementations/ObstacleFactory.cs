using Assets.Script.Chunks;
using Zenject;

namespace Factories.FactoryImplementations
{
    public class ObstacleFactory : IFactory<ObstacleId, ChunkElement>
    {
        public ChunkElement Create(ObstacleId param)
        {
            throw new System.NotImplementedException();
        }
    }
}
