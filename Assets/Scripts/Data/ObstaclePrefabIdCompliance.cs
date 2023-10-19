using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct ObstaclePrefabIdCompliance
    {
        [SerializeField] private string obstacleId;

        [SerializeField] private string prefabPath;

        public string ObstacleId => obstacleId;

        public string PrefabPath => prefabPath;
    }
}