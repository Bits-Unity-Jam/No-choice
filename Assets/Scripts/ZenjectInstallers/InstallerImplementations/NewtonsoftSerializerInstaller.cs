using Zenject;

namespace ZenjectInstallers.InstallerImplementations
{
    public class NewtonsoftSerializerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISerializer>().To<NewtonsoftSerializer.NewtonsoftJsonSerializer>().AsSingle().NonLazy();
        }
    }
}