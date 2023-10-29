using System;
using UnityEngine;

namespace Assets.Script.Chunks
{
    [Serializable]
    public struct ChunkMemberData
    {
        [SerializeField]
        private int _index;

        [SerializeField]
        private Vector2 _initLocalPosition;

        [SerializeField]
        private Quaternion _initLocalRotation;

        [SerializeField]
        private Rigidbody2D _memberRb;
        [SerializeField]
        private Vector3 _startScale;

        public ChunkMemberData(Rigidbody2D memberRb, Vector2 initPosition, int index, Quaternion initLocalRotation, Vector3 startScale)
        {
            _memberRb = memberRb;
            _initLocalPosition = initPosition;
            _index = index;
            _initLocalRotation = initLocalRotation;
            _startScale = startScale;
        }

        public int Index { get => _index; set => _index = value; }
        public Vector2 InitLocalPosition { get => _initLocalPosition; set => _initLocalPosition = value; }
        public Rigidbody2D MemberRb { get => _memberRb; set => _memberRb = value; }
        public Quaternion InitLocalRotation { get => _initLocalRotation; set => _initLocalRotation = value; }
        public Vector3 StartScale { get => _startScale; set => _startScale = value; }
    }
}