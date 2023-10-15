using UnityEngine;

public class VerticalParallax : MonoBehaviour
{
    public float parallaxSpeed = 1.0f;
    public float repeatOffset = 10.0f; // The distance at which layers repeat
    public Transform cameraTransform;
    private Vector3 previousCameraPosition;
    private float initialYPosition;

    void Start()
    {
        //cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        initialYPosition = transform.localPosition.y;
    }

    void FixedUpdate()
    {
        float deltaY = cameraTransform.localPosition.y - previousCameraPosition.y;

        // Move the layer based on parallax speed
        Vector3 newPosition = transform.localPosition + Vector3.up * deltaY * parallaxSpeed;
        transform.localPosition = newPosition;

        // Check if the layer has moved out of the screen
        if (Mathf.Abs(transform.localPosition.y - initialYPosition) >= repeatOffset)
        {
            // Move the layer back to its initial position
            transform.localPosition = new Vector3(transform.localPosition.x, initialYPosition, transform.localPosition.z);
        }

        previousCameraPosition = cameraTransform.position;
    }
}
