using System.Collections.Generic;
using UnityEngine;

namespace Effects.Parallax
{
    public class ParallaxBackgroundService : MonoBehaviour
    {
        [SerializeField] private List<ParallaxBackgroundElement> parallaxBackgroundElements;

        [SerializeField] private float generalMotionSpeed = 1;

        void LateUpdate() => parallaxBackgroundElements.ForEach(HandleParallaxElementBehavior);

        private void HandleParallaxElementBehavior(ParallaxBackgroundElement element)
        {
            float deltaY = generalMotionSpeed * Time.deltaTime;

            // Move the layer based on parallax speed
            Vector3 newPosition = element.ElementTransform.localPosition + Vector3.up * deltaY * element.ElementRelativeParallaxSpeed;
            element.ElementTransform.localPosition = newPosition;

            // Check if the layer has moved out of the screen
            if (Mathf.Abs(element.ElementTransform.localPosition.y - element.ElementInitialPosition.y) >= element.ElementRepeatOffset)
            {
                // Move the layer back to its initial position
                var newLocalPosition = element.ElementTransform.localPosition;
                newLocalPosition =
                    new Vector3(newLocalPosition.x, element.ElementInitialPosition.y, newLocalPosition.z);
            
                element.ElementTransform.localPosition = newLocalPosition;
            }
        }
    }
}