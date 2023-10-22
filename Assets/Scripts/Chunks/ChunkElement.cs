using Chunks;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Script.Chunks
{
    public class ChunkElement : MonoBehaviour, IActivateable, IDeactivateable
    {
        [SerializeField]
        private ChunkElementData _chunkElementData;

        private IActivateable _activateable;
        private IDeactivateable _deactivateable;

        public ChunkElementData ActiveChunkElementData
        {
            get
            {
                if (_chunkElementData.LocalPosition != default
                    || _chunkElementData.LocalRotation != default
                    || _chunkElementData.Scale != default)
                {
                    return _chunkElementData;
                }

                UpdateElementData();

                return _chunkElementData;
            }
            set => _chunkElementData = value;
        }

        [Inject]
        private void Construct(IActivateable activateable, IDeactivateable deactivateable)
        {
            _activateable = activateable;
            _deactivateable = deactivateable;
        }

        public void ApplyData(ChunkElementData chunkElementData, bool hasToActivate = false)
        {
            ActiveChunkElementData = chunkElementData;

            transform.localScale = chunkElementData.Scale;
            transform.localRotation = chunkElementData.LocalRotation;
            transform.localPosition = chunkElementData.LocalPosition;


            gameObject.SetActive(hasToActivate);
        }

        public void Activate() => _activateable.Activate();

        public void Deactivate() => _deactivateable.Deactivate();

        [Button]
        public void UpdateElementData()
        {
            _chunkElementData = new ChunkElementData()
            {
                ObstacleId = _chunkElementData.ObstacleId,
                LocalPosition = transform.localPosition,
                LocalRotation = transform.localRotation,
                Scale = transform.localScale
            };
        }
    }
}
