using Assets.Scripts.Chunks;
using Chunks;
using Zenject;

namespace ZenjectInstallers.InstallerImplementations
{
    public class ObstacleFactoryInstaller : BaseBindFromMethodInstaller<IFactory<ObstacleId, Obstacle>> { }
}