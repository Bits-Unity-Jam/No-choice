using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DrawSprite : MonoBehaviour
{
    [SerializeField] private int thickness = 10;
    [SerializeField] private float notSmoothRadiusPercent = 50.0f;
    [SerializeField] private float refillingSpeed = 0.05f;
    [SerializeField] private float refillingTimeStep = 0.1f;

    private Image image;
    private Texture2D texture;
    private Vector2 point;

    private async void Start()
    {
        image = GetComponent<Image>();
        InitializeTexture();
        StartCoroutine(RefillingAsync());
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 posOnTexture = new Vector2(Input.mousePosition.x * ((float)texture.width / Screen.width), Input.mousePosition.y * ((float)texture.height / Screen.height));
            DrawCircleOnTexture(posOnTexture, Color.clear, thickness);
        }
    }

    private void InitializeTexture()
    {
        int width = Screen.width / 20;
        int height = Screen.height / 20;
        texture = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.black;
        }
        texture.SetPixels(pixels);
        texture.Apply();
        image.sprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
    }

    private void DrawCircleOnTexture(Vector2 position, Color color, int radius)
    {
        int centerX = (int)position.x;
        int centerY = (int)position.y;
        float smoothDistance = radius * (notSmoothRadiusPercent / 100.0f);
        float endDist = radius - smoothDistance;

        centerX = Mathf.Clamp(centerX, radius, texture.width - radius);
        centerY = Mathf.Clamp(centerY, radius, texture.height - radius);

        Color[] colors = texture.GetPixels(centerX - radius, centerY - radius, 2 * radius, 2 * radius);

        for (int x = 0; x < 2 * radius; x++)
        {
            for (int y = 0; y < 2 * radius; y++)
            {
                point.x = centerX - radius + x;
                point.y = centerY - radius + y;
                float distance = Vector2.Distance(point, position);

                if (distance < radius)
                {
                    if (point.x < 0 || point.x >= texture.width || point.y < 0 || point.y >= texture.height)
                    {
                        continue;
                    }

                    if (distance >= smoothDistance)
                    {
                        float delta = distance - smoothDistance;
                        float smoothPercentage = delta / endDist;
                        color.a = smoothPercentage;
                        if (color.a < colors[y * 2 * radius + x].a)
                        {
                            colors[y * 2 * radius + x] = color;
                        }
                    }
                    else if (smoothDistance > distance)
                    {
                        colors[y * 2 * radius + x].a = 0;
                    }
                }
            }
        }
        texture.SetPixels(centerX - radius, centerY - radius, 2 * radius, 2 * radius, colors);
        texture.Apply();
    }

    private IEnumerator  RefillingAsync()
    {
        while (true)
        {
            Color[] colors = texture.GetPixels();
            bool needApply = false;
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i].a < 1.0f)
                {
                    colors[i].a += refillingSpeed;
                    needApply = true;
                }
            }
            if (needApply)
            {
                texture.SetPixels(colors);
                texture.Apply();
            }
           yield return new WaitForSeconds(refillingTimeStep * 1000);
        }
    }
}
