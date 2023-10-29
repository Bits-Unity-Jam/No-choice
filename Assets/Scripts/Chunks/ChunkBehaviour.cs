using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chunks;
using UnityEngine;

namespace Assets.Script.Chunks
{
    public class ChunkBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<Obstacle> chunkMembers;

        private void OnValidate()
        {
            chunkMembers ??= GetComponentsInChildren<Obstacle>().ToList();
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