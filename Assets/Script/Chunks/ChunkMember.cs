﻿using UnityEngine;

namespace Assets.Script.Chunks
{
    public class ChunkMember : MonoBehaviour
    {
        [SerializeField]
        private ChunkMemberData _activeChunkMemberData;

        [SerializeField]
        private ChunkMemberData _autoGeneratedInitData;

        public ChunkMemberData AutoGeneratedInitData { get => _autoGeneratedInitData; private set => _autoGeneratedInitData = value; }
        public ChunkMemberData ActiveChunkMemberData { get => _activeChunkMemberData; set => _activeChunkMemberData = value; }

        private void Awake()
        {
            AutoGeneratedInitData = GenerateInitData();
            ActiveChunkMemberData = AutoGeneratedInitData;
        }

        public ChunkMemberData GenerateInitData()
        {
            ChunkMemberData generatedInitData = new ChunkMemberData()
            {
                Index = transform.GetSiblingIndex(),
                InitLocalPosition = transform.localPosition,
                InitLocalRotation = transform.localRotation,
                MemberRb = GetComponent<Rigidbody2D>(),
            };

            return generatedInitData;
        }

        public void ApplyData(ChunkMemberData chunkMemberData)
        {
            ActiveChunkMemberData = chunkMemberData;
        }

        public void ResetState()
        {
            gameObject.SetActive(true);
            if (_autoGeneratedInitData.MemberRb == default)
            {

                AutoGeneratedInitData = GenerateInitData();
                ActiveChunkMemberData = AutoGeneratedInitData;
            }
            transform.localRotation = ActiveChunkMemberData.InitLocalRotation;
            transform.localPosition = ActiveChunkMemberData.InitLocalPosition;
            ActiveChunkMemberData.MemberRb.velocity = default;
            ActiveChunkMemberData.MemberRb.Sleep();
            ActiveChunkMemberData.MemberRb.isKinematic = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            ActiveChunkMemberData.MemberRb.isKinematic = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

            ActiveChunkMemberData.MemberRb.isKinematic = false;
        }
    }
}