using UnityEngine;
using Zenject;

namespace Assets.Scripts.Chunks
{
    public class BaseBindInstanceInstaller<T> : MonoInstaller
    {
        [SerializeField]
        private T targetImplementation;

        public override void InstallBindings()
        {
            Container.BindInstance(targetImplementation).AsSingle().NonLazy();
        }
    }
}