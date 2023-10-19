using Mechanics.Pools;
using Pools.Containers;
using Pools.Sources;
using UnityEngine;
using Zenject;

namespace Pools.BasePools
{
    public sealed class ObjectPool : MonoBehaviour
    {
        [SerializeField] private int poolItemsCount = 10;

        [SerializeField] private BaseContainer _container;
        
        [SerializeField] private BaseObjectSource<PoolObject> _baseObjectSource;

        [SerializeField] private int index;

        [SerializeField] private bool hasToInjectAtCreation = true;

        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer container)
        {
            _diContainer = container;
        }

        private void Awake()
        {
            _container = GetComponent<BaseContainer>();
            _baseObjectSource = GetComponent<BaseObjectSource<PoolObject>>();
        }

        private void Start()
        {
            InstantiateItems();
        }

        private void InstantiateItems()
        {
            for (int i = 0; i < poolItemsCount; i++)
            {
                PoolObject newItem = Instantiate(_baseObjectSource.GetObject(), transform);
                if (hasToInjectAtCreation)
                {
                    _diContainer.InjectGameObject(newItem.gameObject);
                }

                newItem.Initialize(this);
                newItem.PushToPool();
                newItem.gameObject.name = $"{newItem.gameObject.name} [{index}]";
                index++;
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
                InstantiateItems();
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