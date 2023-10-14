using UnityEngine;
using UnityEngine.UI;

public class DrawSprite : MonoBehaviour
{
    private Image image;
    private Texture2D texture;

    private void Start()
    {
        // Get the SpriteRenderer component
        image = GetComponent<Image>();
        // Initialize the texture and set it as the sprite for the SpriteRenderer
        InitializeTexture();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Check for mouse input (left click) and draw a red circle on the texture.
            DrawCircleOnTexture(Input.mousePosition, Color.clear, 50);
        }
    }

    private void InitializeTexture()
    {
        texture = new Texture2D(Screen.width, Screen.height);
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, Color.black); 
            }
        }
        texture.Apply(); 

        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
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
                    if (x <= 0 || x >= Screen.width || y <= 0 || y >= Screen.height)
                    {

                    }
                    else
                    {
                        /*// Circle center coordinates
                        Vector2 circleCenter = new Vector2(centerX, centerY);

                        // Point coordinates
                        Vector2 point = new Vector2(x, y);
                        float distance = Vector2.Distance(point, circleCenter);

                        float percentage = Mathf.Min((distance / radius),1.0f) * 100;

                        color.a = (percentage / 100) * 255;*/
                        texture.SetPixel(x, y, color);
                    }
                }
            }
        }

        texture.Apply(); 
    }
}

