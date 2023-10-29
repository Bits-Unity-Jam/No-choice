using Data;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Serialization;
using Zenject;

namespace Assets.Scripts.Pools.BasePools.Sources
{
    public class ObstacleObjectSource : SingleObjectSource<PoolObject>
    {
        [SerializeField]
        private ObstacleId obstacleId;

        private PoolObject _foundPoolObject;

        private ObstacleIdPrefabDatabase _obstacleIdPrefabDatabase;
        public ObstacleId ObstacleId { get => obstacleId; set => obstacleId = value; }
        
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
                    _obstacleIdPrefabDatabase.GetObstacleWithId(obstacleId);

                AsyncOperationHandle<Object> loadedObject = 
                    Addressables.LoadAssetAsync<Object>(obstacleIdPrefabCompliance.PrefabPath);

                await loadedObject.Task;

                _foundPoolObject = loadedObject.Result.GetComponent<PoolObject>();
            }

            return _foundPoolObject;
        }
    }
}

