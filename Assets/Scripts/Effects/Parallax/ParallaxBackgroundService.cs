using System.Collections.Generic;
using UnityEngine;

namespace Effects.Parallax
{
    public class ParallaxBackgroundService : MonoBehaviour
    {
        [SerializeField] private List<ParallaxBackgroundLayoutElement> parallaxBackgroundElements;

        [SerializeField] private float generalMotionSpeed = 1;

        void LateUpdate() => parallaxBackgroundElements.ForEach(HandleParallaxElementBehavior);

        private void HandleParallaxElementBehavior(ParallaxBackgroundLayoutElement layoutElement)
        {
            float deltaY = generalMotionSpeed * Time.deltaTime;

            // Move the layer based on parallax speed
            Vector3 newPosition = layoutElement.ElementTransform.localPosition + Vector3.up * deltaY * layoutElement.ElementRelativeParallaxSpeed;
            layoutElement.ElementTransform.localPosition = newPosition;

            // Check if the layer has moved out of the screen
            if (Mathf.Abs(layoutElement.ElementTransform.localPosition.y - layoutElement.ElementInitialPosition.y) >= layoutElement.ElementRepeatOffset)
            {
                // Move the layer back to its initial position
                var newLocalPosition = layoutElement.ElementTransform.localPosition;
                newLocalPosition =
                    new Vector3(newLocalPosition.x, layoutElement.ElementInitialPosition.y, newLocalPosition.z);
            
                layoutElement.ElementTransform.localPosition = newLocalPosition;
            }
        }
    }
}