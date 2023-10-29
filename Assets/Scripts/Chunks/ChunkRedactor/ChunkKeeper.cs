using System;
using System.Collections.Generic;
using System.IO;
using DataStorage;
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
    public class ChunkKeeper : MonoBehaviour
    {
        [SerializeField] private Transform _createdObjectsParentTransform;
        [SerializeField] private ObstacleCreator _obstacleCreator;

        private const string CHUNK_SAVE_PATH = "/Resources/Chunks/";
        private List<Obstacle> _foundObstacle;
        private List<ObstacleData> _foundObstacleDatas;
        private ISerializer _serializer;
        private IStorage _storage;

        [Space, SerializeField, Header("Index to save/load the chunk:")] private int _chunkIndex;

        public int ChunksNumber => Directory.GetFiles(Application.dataPath + CHUNK_SAVE_PATH).Length;
        
        [Inject]
        private void Construct(ISerializer serializer,  IStorage storage)
        {
            _serializer = serializer;
            _storage = storage;
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
                obstacle.IsChunkRedactorModeActive = true;
                _foundObstacleDatas.Add(obstacle.ObstacleData);
            }

            ChunkData chunkData = new ChunkData()
                { ChunkIndex = _chunkIndex, ChunkObstaclesData = _foundObstacleDatas };
            
            string serialized = _serializer.Serialize(chunkData);
           
            _storage.SaveAs(serialized, $"{CHUNK_SAVE_PATH}/Chunk_{_chunkIndex}"); 
        }

        public List<Obstacle> Load() => Load(_chunkIndex);

        public List<Obstacle> Load(int chunkIndex)
        {
            _obstacleCreator.Clear();
            
            string loadedChunkDataString  = _storage.Load($"{CHUNK_SAVE_PATH}/Chunk_{chunkIndex}");

            ChunkData loadedChunkData = _serializer.Deserialize<ChunkData>(loadedChunkDataString);

            List<Obstacle> createdObstacles = new();
            
            loadedChunkData.ChunkObstaclesData.ForEach(obstacle =>
               createdObstacles.Add(_obstacleCreator.Create(obstacle)));
            
            return createdObstacles;
        }
    }
}