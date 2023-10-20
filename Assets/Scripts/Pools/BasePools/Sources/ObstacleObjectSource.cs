using Data;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Assets.Scripts.Pools.BasePools.Sources
{
    public class ObstacleObjectSource : SingleObjectSource<PoolObject>
    {
        [SerializeField]
        private ObstacleId ObstacleId;

        private PoolObject _foundPoolObject;

        private ObstacleIdPrefabDatabase _obstacleIdPrefabDatabase;

        [Inject]
        private void Construct(ObstacleIdPrefabDatabase obstacleIdPrefabDatabase) 
        {
            _obstacleIdPrefabDatabase = obstacleIdPrefabDatabase;
        }

        public async override Task<PoolObject> GetObjectAsync()
        {
            if (_foundPoolObject == default)
            {
                ObstaclePrefabIdCompliance obstacleIdPrefabCompliance = 
                    _obstacleIdPrefabDatabase.GetObstacleWithId(ObstacleId);

                AsyncOperationHandle<Object> loadedObject = 
                    Addressables.LoadAssetAsync<Object>(obstacleIdPrefabCompliance.PrefabPath);

                await loadedObject.Task;

                _foundPoolObject = loadedObject.Result.GetComponent<PoolObject>();
            }

            return _foundPoolObject;
        }
    }
}

