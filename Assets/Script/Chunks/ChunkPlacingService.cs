using Assets.Script.Chunks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChunkPlacingService : MonoBehaviour
{
    [SerializeField]
    private Vector3 _placingOffset;

    [SerializeField]
    private Transform _placingOriginPosition;

    [SerializeField]
    private List<ChunkBehaviour> _chunkBehaviours;

    [Range(0, 100)]
    [SerializeField]
    private float minimumChunkRange;

    [Range(0, 100)]
    [SerializeField]
    private float maximumChunkRange;

    private float _generatedkDistanceToNextChunk;
    private float _nextChunkHigh;
    private float _lastChunkHigh;
    private int _lastChunkIndex;

    private void OnValidate()
    {
        if (maximumChunkRange < minimumChunkRange) maximumChunkRange = minimumChunkRange;
    }

    private void Update()
    {
        if(_nextChunkHigh - _lastChunkHigh > _generatedkDistanceToNextChunk)
        {
            _lastChunkHigh = _generatedkDistanceToNextChunk;

            _lastChunkIndex = GenerateChunkIndex();
            _generatedkDistanceToNextChunk = GenerateDistanceToNextChunk();

            _chunkBehaviours[_lastChunkIndex].PlaceAtHighRelateOrigin(_placingOriginPosition.position, _generatedkDistanceToNextChunk);
        }

    }

    private int GenerateChunkIndex() => Random.Range(0, _chunkBehaviours.Count);

    private float GenerateDistanceToNextChunk() => Random.Range(minimumChunkRange, maximumChunkRange);
}
