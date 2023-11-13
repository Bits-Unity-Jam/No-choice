using DataStructures.Sourses;
using Pools.Containers;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Pools.BasePools
{
    public sealed class ObjectPool : MonoBehaviour
    {
        [SerializeField] private int poolItemsCount = 10;

        [SerializeField] private BaseContainer _container;

        [SerializeField] private int lastCreatedObjectIndex;

        [SerializeField] private bool hasToInjectAtCreation = true;

        [SerializeField, Space] private BaseObjectSource<PoolObject> _sourceObject;

        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer container)
        {
            _diContainer = container;
        }

        private void OnValidate()
        {
            InitializePoolData();
        }

        private void InitializePoolData()
        {
            _container ??= GetComponent<BaseContainer>();
        }

        private void Awake()
        {
            InstantiateItems(poolItemsCount);
        }

        private async void InstantiateItems(int count = 5)
        {
            for (int i = 0; i < count; i++)
            {
                var objectToCreate = await _sourceObject.GetObjectAsync();

                PoolObject newItem = Instantiate(objectToCreate, transform);
                if (hasToInjectAtCreation)
                {
                    _diContainer.InjectGameObject(newItem.gameObject);
                }

                newItem.Initialize(this);
                newItem.PushToPool();
                newItem.gameObject.name = $"{newItem.gameObject.name} [{lastCreatedObjectIndex}]";
                lastCreatedObjectIndex++;
            }
        }

        public void PushObject(PoolObject poolObject)
        {
            var poolObjectTransform = poolObject.transform;

            poolObjectTransform.parent = transform;
            poolObjectTransform.position = transform.position;

            _container.PushObject(poolObject);
        }

        public PoolObject PullObject()
        {
            if (_container != null && _container.GetLength() <= 1)
            {
                InstantiateItems(poolItemsCount);
            }

            if ((_container.PullObject() == default))
            {
                return default;
            }

            
            return _container.PullObject().Pull();
        }

        public PoolObject PullRandomObject()
        {
            if (_container != null && _container.GetLength() <= 1)
            {
                InstantiateItems();
            }

            return _container.PullObject().Pull();
        }

        public int GetCurrentSize()
        {
            return _container.GetLength();
        }

        public int GetStartSize()
        {
            return poolItemsCount;
        }
    }
}