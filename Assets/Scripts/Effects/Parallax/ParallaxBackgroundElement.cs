using System;
using UnityEngine;

namespace Effects.Parallax
{
    [Serializable]
    public struct ParallaxBackgroundElement
    {
        [SerializeField] private Transform elementTransform;

        [SerializeField] private Vector3 elementInitialPosition;

        [SerializeField] private float elementRepeatOffset;

        [SerializeField, Range(-10, 10)] private float elementRelativeParallaxSpeed;

        public ParallaxBackgroundElement(Transform elementTransform, Vector3 elementInitialPosition, float elementRepeatOffset, float elementRelativeParallaxSpeed)
        {
            this.elementTransform = elementTransform;
            this.elementInitialPosition = elementInitialPosition;
            this.elementRepeatOffset = elementRepeatOffset;
            this.elementRelativeParallaxSpeed = elementRelativeParallaxSpeed;
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
            get => elementRelativeParallaxSpeed;
            set => elementRelativeParallaxSpeed = value;
        }

    }
}