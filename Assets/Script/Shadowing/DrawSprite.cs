using Game.Energy;
using Game.Energy.UI;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DrawSprite : MonoBehaviour
{

    [SerializeField] private int _thickness = 10;
    [SerializeField] private float _notSmoothRadiusPrecenteg = 50.0f;
    [SerializeField] private float _refillingSpeed = 0.05f;
    [SerializeField] private float _refillingTimeStep = 0.1f;

    private Image image;
    private Texture2D texture;

    private Vector2 point;



    private void Start()
    {
        image = GetComponent<Image>();
        InitializeTexture();
        StartCoroutine(Refilling());
    }


    private  void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 posOnTexture = new Vector2(Input.mousePosition.x * ((float)texture.width / Screen.width),Input.mousePosition.y * ((float)texture.height / Screen.height));
            DrawCircleOnTexture(posOnTexture, Color.clear, _thickness);
        }
    }

    private void InitializeTexture()
    {
        texture = new Texture2D(Screen.width/20, Screen.height/ 20);
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

    private  void  DrawCircleOnTexture(Vector2 position, Color color, int radius)
    {
        int centerX = (int)position.x;
        int centerY = (int)position.y;

        for (int x = centerX - radius; x < centerX + radius; x++)
        {
            for (int y = centerY - radius; y < centerY + radius; y++)
            {
               point = new Vector2(x, y);
                float distance = Vector2.Distance(point, position);

                if(distance < radius)
                {
                    if (x <= 0 || x >= texture.width || y <= 0 || y >= texture.height)
                    {

                    }
                    else
                    {
                        float smoothDistance = radius * (_notSmoothRadiusPrecenteg / 100.0f);
                        if (distance >= smoothDistance)
                        {
                            float delta = distance - smoothDistance;
                            float endDist = radius - smoothDistance;
                            float smoothPrecentage = delta / endDist;
                            color.a = smoothPrecentage;
                            if(color.a < texture.GetPixel(x,y).a)
                            {
                                texture.SetPixel(x, y,color);
                            }
                        }
                        else if (smoothDistance > distance)
                        {
                            color.a = 0;
                            texture.SetPixel(x, y, color);
                        }
                    }
                } 
            }
        }
        texture.Apply();
    }

    private IEnumerator Refilling()
    {
        while (true)
        {
            Color[] colors = texture.GetPixels();
            bool needApply = false;
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    if (colors[y * texture.width + x].a < 1.0f)
                    {
                        colors[y * texture.width + x].a += _refillingSpeed;
                        needApply = true;
                    }
                }
            }
            if (needApply)
            {
                texture.SetPixels(colors);
                texture.Apply();
            }
            yield return new WaitForSeconds(_refillingTimeStep);
        }
        
    }
}

