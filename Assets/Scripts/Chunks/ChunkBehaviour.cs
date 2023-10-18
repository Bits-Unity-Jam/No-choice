using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Script.Chunks
{
    public class ChunkBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<ChunkMember> chunkMembers;

        private void OnValidate()
        {
            chunkMembers ??= GetComponentsInChildren<ChunkMember>().ToList();
        }

        public void ResetInitialState()
        {
            gameObject.SetActive(true);
            chunkMembers.ForEach(member => member.ResetState());
        }

        public void PlaceAtHighRelateOrigin(Vector3 originPosition, float generatedkDistanceToNextChunk)
        {
            ResetInitialState();
            transform.position = new Vector3(originPosition.x,
                originPosition.y + generatedkDistanceToNextChunk, originPosition.z);
        }
    }
}