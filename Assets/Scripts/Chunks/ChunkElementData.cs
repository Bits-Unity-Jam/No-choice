using System;
using UnityEngine;

namespace Chunks
{
    [Serializable]
    public struct ChunkElementData
    {
        [SerializeField] private string obstacleId;

        [SerializeField] private Vector3 localPosition;

        [SerializeField] private Quaternion localRotation;

        [SerializeField] private Vector3 scale;
    }
}
