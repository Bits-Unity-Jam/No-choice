using UnityEngine;
using Zenject;

namespace Assets.Scripts.Chunks
{
    public class BaseBindFromMethodInstaller<T> : MonoInstaller
    {
        //the game object which contains <T>
        [SerializeField]
        private GameObject targetImplementation;

        public override void InstallBindings() => Bind();

        private void Bind() =>
            Container.
                Bind<T>().
                FromMethod(() => targetImplementation.GetComponent<T>()).
                AsSingle().NonLazy();
    }

}