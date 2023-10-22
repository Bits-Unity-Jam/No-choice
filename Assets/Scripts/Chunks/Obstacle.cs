﻿using System;
using Assets.Script.Chunks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Chunks
{
    public class Obstacle : MonoBehaviour, IActivateable, IDeactivateable, IDefaultStateReturner
    {
        [FormerlySerializedAs("_chunkElementData")] [SerializeField]
        private ObstacleData obstacleData;

        private IActivateable _activateable;
        private IDeactivateable _deactivateable;
        private IDefaultStateReturner _defaultStateReturner;

        public ObstacleData ActiveObstacleData
        {
            get
            {
                if (obstacleData.LocalPosition != default
                    || obstacleData.LocalRotation != default
                    || obstacleData.Scale != default)
                {
                    return obstacleData;
                }

                UpdateElementData();

                return obstacleData;
            }
            set => obstacleData = value;
        }

        [Inject]
        private void Construct(IActivateable activateable, IDeactivateable deactivateable, IDefaultStateReturner defaultStateReturner)
        {
            _activateable = activateable;
            _deactivateable = deactivateable;
            _defaultStateReturner = defaultStateReturner;
        }

        public void ApplyData(ObstacleData obstacleData, bool hasToActivate = false)
        {
            ActiveObstacleData = obstacleData;

            transform.localScale = obstacleData.Scale;
            transform.localRotation = obstacleData.LocalRotation;
            transform.localPosition = obstacleData.LocalPosition;


            gameObject.SetActive(hasToActivate);
        }

        public void Activate() => _activateable.Activate();

        public void Deactivate() => _deactivateable.Deactivate();

        [Button]
        public void UpdateElementData()
        {
            obstacleData = new ObstacleData()
            {
                ObstacleId = obstacleData.ObstacleId,
                LocalPosition = transform.localPosition,
                LocalRotation = transform.localRotation,
                Scale = transform.localScale
            };
        }

        public void ReturnToDefaultState()
        {
            _defaultStateReturner.ReturnToDefaultState();
        }
    }
}