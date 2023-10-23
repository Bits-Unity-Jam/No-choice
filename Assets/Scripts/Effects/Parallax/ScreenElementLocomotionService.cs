using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Effects.Parallax
{
    public class ScreenElementLocomotionService : MonoBehaviour
    {
        [FormerlySerializedAs("parallaxBackgroundElements")] [SerializeField] private List<RepeatableElement> elements;

        [SerializeField] private float generalMotionSpeed = 1;
        
        public event Action<RepeatableElement> OnElementPositionReset;
        
        private void LateUpdate() => elements.ForEach(HandleParallaxElementBehavior);

        private void HandleParallaxElementBehavior(RepeatableElement layoutElement)
        {
            float deltaY = generalMotionSpeed * Time.deltaTime;

            // Move the layer based on parallax speed
            Vector3 newPosition = layoutElement.ElementTransform.localPosition + Vector3.up * deltaY * layoutElement.ElementRelativeParallaxSpeed;
            layoutElement.ElementTransform.localPosition = newPosition;

            // Check if the layer has moved out of the screen
            if (Mathf.Abs(layoutElement.ElementTransform.localPosition.y - layoutElement.ElementInitialPosition.y) > layoutElement.ElementRepeatOffset)
            {
                ResetInitialPosition(layoutElement);
            }
        }

        private void ResetInitialPosition(RepeatableElement layoutElement)
        {
            // Move the layer back to its initial position
            var newLocalPosition = layoutElement.ElementTransform.localPosition;
            newLocalPosition =
                new Vector3(newLocalPosition.x, layoutElement.ElementInitialPosition.y, newLocalPosition.z);

            layoutElement.ElementTransform.localPosition = newLocalPosition;
            
            OnElementPositionReset?.Invoke(layoutElement);
        }
    }
}