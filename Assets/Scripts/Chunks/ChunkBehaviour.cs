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
        private List<ChunkElement> chunkMembers;

        private void OnValidate()
        {
            chunkMembers ??= GetComponentsInChildren<ChunkElement>().ToList();
        }

        public void ResetInitialState()
        {
            gameObject.SetActive(true);
            chunkMembers.ForEach(member => member.Deactivate());
        }

        public void SpawnAtHeight(float height)
        {
            ResetInitialState();
            transform.position = new Vector3(transform.position.x,
                height, transform.position.z);
        }
        public void PlaceAtHeight(float height)
        {
            transform.position = new Vector3(transform.position.x,
                height, transform.position.z);
        }
    }
}