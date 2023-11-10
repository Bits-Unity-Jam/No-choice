using System;
using System.Linq;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Create ShieldDatabase", fileName = "ScriptableObjects/ShieldDatabase", order = 0)]
    public class ShieldDatabase : ScriptableObject
    {
        [SerializeField] private ShieldData[] _obstaclePrefabIdCompliances;


        public ShieldData GetItemWithId(ShieldID shieldID)
        {
            ShieldData databaseItemPath =
                _obstaclePrefabIdCompliances.FirstOrDefault(compliance => compliance.ShieldID == shieldID);

            return databaseItemPath;
        }
    }

    public enum ShieldID
    {
        Viking,
        Ukraine,
        Ender, 
        Knight, 
        PumpkinSmall, 
        PumpkinBig, 
        Skull, 
        Snowflake, 
        Wreath
        
    }
    
    [Serializable]
    public struct ShieldData
    {
        [SerializeField] private ShieldID _shieldID;
        [SerializeField] private string _shieldIconPath;
        [SerializeField] private string _shieldPrefabPath;
        [SerializeField] private int _shieldPrice;

        public string ShieldPrefabPath => _shieldPrefabPath;

        public int ShieldPrice => _shieldPrice;

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