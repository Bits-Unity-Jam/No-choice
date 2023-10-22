using System;
using UnityEngine;

namespace Chunks.ChunkRedactor
{
    [RequireComponent(typeof(ObstacleCreator))]
    public class ChunkBuilder : MonoBehaviour
    {
        [SerializeField] private int _chunkIndex;
        [SerializeField] private Transform _createdObjectsParentTransform;
        [SerializeField] private ObstacleCreator _obstacleCreator;

        private void OnValidate()
        {
            _obstacleCreator ??= GetComponent<ObstacleCreator>();
        }

        public void Save()
        {
        }

        public void Load()
        {
        }
    }
}