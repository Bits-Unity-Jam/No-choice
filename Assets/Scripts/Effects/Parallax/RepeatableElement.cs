using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Effects.Parallax
{
    [Serializable]
    public struct RepeatableElement
    {
        [SerializeField] private Transform elementTransform;

        [SerializeField] private Vector3 elementInitialPosition;

        [SerializeField] private float elementRepeatOffset;

        [FormerlySerializedAs("elementRelativeParallaxSpeed")] [SerializeField, Range(-10, 10)] private float elementRelativeSpeed;

        public RepeatableElement(Transform elementTransform, Vector3 elementInitialPosition, float elementRepeatOffset, float elementRelativeSpeed)
        {
            this.elementTransform = elementTransform;
            this.elementInitialPosition = elementInitialPosition;
            this.elementRepeatOffset = elementRepeatOffset;
            this.elementRelativeSpeed = elementRelativeSpeed;
        }

        public Transform ElementTransform
        {
            get => elementTransform;
            set => elementTransform = value;
        }

        public Vector3 ElementInitialPosition
        {
            get => elementInitialPosition;
            set => elementInitialPosition = value;
        }

        public float ElementRepeatOffset
        {
            get => elementRepeatOffset;
            set => elementRepeatOffset = value;
        }

        public float ElementRelativeParallaxSpeed
        {
            get => elementRelativeSpeed;
            set => elementRelativeSpeed = value;
        }

    }
}