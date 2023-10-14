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
        private List<ChunkMemberData> _chunks;

        [SerializeField]
        private List<ChunkMember> chunkMembers;

        private void OnValidate()
        {
            chunkMembers ??= GetComponentsInChildren<ChunkMember>().ToList();
        }

        public void ResetInitialState()
        {
            chunkMembers.ForEach(member => member.ResetState());
        }

        public void PlaceAtHighRelateOrigin(Vector3 originPosition, float generatedkDistanceToNextChunk)
        {
            transform.position = new Vector3(originPosition.x,
                originPosition.y + generatedkDistanceToNextChunk, originPosition.z);
        }
    }
}