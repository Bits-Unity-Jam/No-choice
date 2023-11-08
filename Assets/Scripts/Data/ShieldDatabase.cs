using System;
using System.Linq;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Create ShieldDatabase", fileName = "ScriptableObjects/ShieldDatabase", order = 0)]
    public class ShieldDatabase : ScriptableObject
    {
        [SerializeField] private ShieldData[] _obstaclePrefabIdCompliances;


        public ShieldData GetObstacleWithId(ShieldID shieldID)
        {
            ShieldData databaseItemPath =
                _obstaclePrefabIdCompliances.FirstOrDefault(compliance => compliance.ShieldID == shieldID);

            return databaseItemPath;
        }
    }

    public enum ShieldID
    {
        defaultShield
    }
    
    [Serializable]
    public struct ShieldData
    {
        [SerializeField] private ShieldID _shieldID;
        [SerializeField] private string _shieldIconPath;

        public string ShieldIconPath
        {
            get => _shieldIconPath;
            set => _shieldIconPath = value;
        }

        public ShieldID ShieldID
        {
            get => _shieldID;
            set => _shieldID = value;
        }
    }
}