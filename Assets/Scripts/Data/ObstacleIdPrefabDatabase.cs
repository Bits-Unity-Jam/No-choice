using System.Linq;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ObstacleIdPrefabDatabase", menuName = "ScriptableObjects/ObstacleIdPrefabDatabase",
        order = 1)]
    public class ObstacleIdPrefabDatabase : ScriptableObject
    {
        [SerializeField] private ObstaclePrefabIdCompliance[] _obstaclePrefabIdCompliances;


        public ObstaclePrefabIdCompliance GetObstacleWithId(ObstacleId obstacleId)
        {
            ObstaclePrefabIdCompliance databaseItemPath =
                _obstaclePrefabIdCompliances.FirstOrDefault(compliance => compliance.ObstacleId == obstacleId);

            return databaseItemPath;
        }
    }
}