using UnityEngine;

public class DrawSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Texture2D texture;

    private void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Initialize the texture and set it as the sprite for the SpriteRenderer
        InitializeTexture();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Check for mouse input (left click) and draw a red circle on the texture.
            DrawCircleOnTexture(Input.mousePosition, Color.clear, 15);
        }
    }

    private void InitializeTexture()
    {
        // Create a new transparent 2D texture
        texture = new Texture2D(512, 512);
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, Color.black); // Clear (transparent) color
            }
        }
        texture.Apply(); // Apply the changes to the texture

        // Assign the texture to the SpriteRenderer
        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    private void DrawCircleOnTexture(Vector2 position, Color color, int radius)
    {
        int centerX = (int)position.x;
        int centerY = (int)position.y;

        for (int x = centerX - radius; x < centerX + radius; x++)
        {
            for (int y = centerY - radius; y < centerY + radius; y++)
            {
                if (Vector2.Distance(new Vector2(x, y), position) < radius)
                {
                    texture.SetPixel(x, y, color);
                }
            }
        }

        texture.Apply(); // Apply the changes to the texture
    }
}

