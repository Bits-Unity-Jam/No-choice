using System;
using UnityEngine;

public enum ObstacleId
{
    Callibre, Drone, Helicopter, Rocker, Soldier
}

namespace Chunks
{
    [Serializable]
    public struct ChunkElementData
    {
        [SerializeField] private ObstacleId _obstacleId;

        [SerializeField] private Vector3 _localPosition;

        [SerializeField] private Quaternion _localRotation;

        [SerializeField] private Vector3 _scale;

        public ChunkElementData(Vector3 scale, Quaternion localRotation, Vector3 localPosition, ObstacleId obstacleId) : this()
        {
            _scale = scale;
            _localRotation = localRotation;
            _localPosition = localPosition;
            _obstacleId = obstacleId;
        }

        public Vector3 Scale { get => _scale; set => _scale = value; }
        public Quaternion LocalRotation { get => _localRotation; set => _localRotation = value; }
        public Vector3 LocalPosition { get => _localPosition; set => _localPosition = value; }
        public ObstacleId ObstacleId { get => _obstacleId; set => _obstacleId = value; }
    }
}
