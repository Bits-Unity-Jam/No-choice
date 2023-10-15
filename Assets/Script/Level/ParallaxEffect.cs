using UnityEngine;

public class VerticalParallax : MonoBehaviour
{
    public float parallaxSpeed = 1.0f;
    public float repeatOffset = 10.0f; // The distance at which layers repeat
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    private float initialYPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        initialYPosition = transform.position.y;
    }

    void Update()
    {
        float deltaY = cameraTransform.position.y - previousCameraPosition.y;

        // Move the layer based on parallax speed
        Vector3 newPosition = transform.position + Vector3.up * deltaY * parallaxSpeed;
        transform.position = newPosition;

        // Check if the layer has moved out of the screen
        if (Mathf.Abs(transform.position.y - initialYPosition) >= repeatOffset)
        {
            // Move the layer back to its initial position
            transform.position = new Vector3(transform.position.x, initialYPosition, transform.position.z);
        }

        previousCameraPosition = cameraTransform.position;
    }
}
