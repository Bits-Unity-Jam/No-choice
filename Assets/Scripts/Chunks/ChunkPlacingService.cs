using System.Collections.Generic;
using Assets.Script.Chunks;
using UnityEngine;

namespace Chunks
{
    
    public class ChunkPlacingService : MonoBehaviour
    {
        [SerializeField] private float generalMotionSpeed = 1;
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

        [SerializeField] private float chunkSpawnHeight;
        private int _previousChunkIndex;

        private void OnValidate()
        {
            if (maximumChunkRange < minimumChunkRange) maximumChunkRange = minimumChunkRange;
        }

        void LateUpdate() => _chunkBehaviours.ForEach(HandleParallaxElementBehavior);

        private void HandleChunkBehavior(ChunkBehaviour chunk)
        {
            float deltaY = generalMotionSpeed * Time.deltaTime;

            // Move the layer based on parallax speed
            Vector3 newPosition = chunk.transform.localPosition + Vector3.up * deltaY;
            
            chunk.transform.localPosition = newPosition;

            // Check if the layer has moved out of the screen
            if (Mathf.Abs(chunk.transform.localPosition.y - chunk.transform.y) >= chunk.ElementRepeatOffset)
            {
                // Move the layer back to its initial position
                var newLocalPosition = chunk.transform.localPosition;
                newLocalPosition =
                    new Vector3(newLocalPosition.x, chunk.transform.y, newLocalPosition.z);
            
                chunk.ElementTransform.localPosition = newLocalPosition;
            }
        }

        private int GenerateChunkIndex() => Random.Range(0, _chunkBehaviours.Count);
    }
}
