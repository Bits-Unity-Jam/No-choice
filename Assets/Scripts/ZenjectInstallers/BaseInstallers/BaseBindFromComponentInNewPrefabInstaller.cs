using UnityEngine;
using Zenject;

namespace Assets.Scripts.Chunks
{
    public class BaseBindFromComponentInNewPrefabInstaller<T> : MonoInstaller
    {
        [SerializeField]
        private Object targetImplementation;

        public override void InstallBindings()
        {
            Container.Bind<T>().FromComponentInNewPrefab(targetImplementation).AsSingle().NonLazy();
        }
    }
}