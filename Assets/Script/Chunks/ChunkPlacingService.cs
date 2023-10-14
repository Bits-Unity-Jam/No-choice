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

    [SerializeField]
    private float _generatedkDistanceToNextChunk;
    [SerializeField]
    private float _nextChunkHigh;
    [SerializeField]
    private float _lastChunkHigh;
    [SerializeField]
    private int _lastChunkIndex;

   
    private void OnValidate()
    {
        if (maximumChunkRange < minimumChunkRange) maximumChunkRange = minimumChunkRange;
    }

    private void Start()
    {
        CreateConfigForNextChunk();
    }

    private void Update()
    {
        if(_placingOriginPosition.position.y - _lastChunkHigh > _generatedkDistanceToNextChunk)
        {
            CreateConfigForNextChunk();
        }
    }

    private void CreateConfigForNextChunk()
    {
        _generatedkDistanceToNextChunk = GenerateDistanceToNextChunk();
        _lastChunkHigh = _nextChunkHigh;
        _nextChunkHigh += _generatedkDistanceToNextChunk;
        _lastChunkIndex = GenerateChunkIndex();

        _chunkBehaviours[_lastChunkIndex].
            PlaceAtHighRelateOrigin((_placingOriginPosition.position + _placingOffset), _generatedkDistanceToNextChunk);
    }

    private int GenerateChunkIndex() => Random.Range(0, _chunkBehaviours.Count);

    private float GenerateDistanceToNextChunk() => Random.Range(minimumChunkRange, maximumChunkRange);
}
