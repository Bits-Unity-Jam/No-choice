using UnityEngine;

public class CameraEadgeDetection : MonoBehaviour
{
    private Camera mainCamera;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        mainCamera = Camera.main;

        // Calculate the size and position of the collider based on the camera's frustum
        float verticalSize = mainCamera.orthographicSize * 2f;
        float horizontalSize = verticalSize * mainCamera.aspect;

        Vector2 colliderSize = new Vector2(horizontalSize, verticalSize);

        // Set the collider size and position
        boxCollider.size = colliderSize;
    }
}