using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Chunks.ChunkRedactor
{
    [Serializable]
    public struct ChunkData
    {
        [SerializeField]
        private int _chunkIndex;
        
        [FormerlySerializedAs("_chunkObstacleDatas")] [SerializeField]
        private List<ObstacleData> chunkObstaclesData;
        
        public int ChunkIndex
        {
            get => _chunkIndex;
            set => _chunkIndex = value;
        }

        public List<ObstacleData> ChunkObstaclesData
        {
            get => chunkObstaclesData;
            set => chunkObstaclesData = value;
        }
        
    }
    
    [RequireComponent(typeof(ObstacleCreator))]
    public class ChunkBuilder : MonoBehaviour
    {
        [SerializeField] private Transform _createdObjectsParentTransform;
        [SerializeField] private ObstacleCreator _obstacleCreator;
        
        private List<Obstacle> _foundObstacle;
        private List<ObstacleData> _foundObstacleDatas;
        private ISerializer _serializer;

        [Space, SerializeField, Header("Index to save/load the chunk:")] private int _chunkIndex;

        [Inject]
        private void Construct(ISerializer serializer)
        {
            _serializer = serializer;
        }
        
        private void OnValidate()
        {
            _obstacleCreator ??= GetComponent<ObstacleCreator>();
        }

        public void Save()
        {
            _foundObstacle = new List<Obstacle>();
            _foundObstacleDatas = new List<ObstacleData>();
            
            foreach (Transform t in _createdObjectsParentTransform)
            {
                _foundObstacle.Add(t.GetComponent<Obstacle>());
            }

            foreach (var obstacle in _foundObstacle)
            {
                _foundObstacleDatas.Add(obstacle.ActiveObstacleData);
            }

            ChunkData chunkData = new ChunkData()
                { ChunkIndex = _chunkIndex, ChunkObstaclesData = _foundObstacleDatas };

            string serialized = _serializer.Serialize(chunkData);
            
        }

        public void Load()
        {
        }
    }
}