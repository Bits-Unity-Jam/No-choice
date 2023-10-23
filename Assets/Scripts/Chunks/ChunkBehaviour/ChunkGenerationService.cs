using System;
using System.Collections.Generic;
using Chunks.ChunkRedactor;
using Effects.Parallax;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

namespace Chunks.ChunkBehaviour
{
    [RequireComponent(typeof(ScreenElementLocomotionService))]
    public class ChunkGenerationService : MonoBehaviour
    {
        [SerializeField] private ScreenElementLocomotionService _screenElementLocomotionService;


        [SerializeField] private ChunkKeeper _chunkKeeper;

        // [Inject]
        // private void Construct(ChunkKeeper chunkKeeper)
        // {
        //     _chunkKeeper = chunkKeeper;
        // }
        
        private void OnValidate()
        {
            _screenElementLocomotionService ??= GetComponent<ScreenElementLocomotionService>();
        }

        private void Start()
        {
            _screenElementLocomotionService.OnElementPositionReset += HandleElementPositionReset;
        }

        private void HandleElementPositionReset(RepeatableElement obj)
        {
            int nextChunkIndex = GetNextChunkIndex();

            List<Obstacle> loadedNextChunk = _chunkKeeper.Load(nextChunkIndex);
            
            foreach (var obstacle in loadedNextChunk)
            {
                obstacle.transform.parent = obj.ElementTransform;
                obstacle.transform.localPosition = obstacle.ObstacleDataWithoutUpdate.LocalPosition.ConvertToVector3();
            }
        }

        private int GetNextChunkIndex()
        {
            return Random.Range(0, 5);
        }
    }
}