using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct ObstaclePrefabIdCompliance
    {
        [SerializeField] private ObstacleId obstacleId;

        [SerializeField] private string prefabPath;

        public ObstacleId ObstacleId => obstacleId;

        public string PrefabPath => prefabPath;
    }
}