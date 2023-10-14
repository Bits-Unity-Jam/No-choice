using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttachMouse : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float error;

    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
 
    void FixedUpdate()
    {
        Vector3 mousePositionScreen = Input.mousePosition;

        
        Vector2 in_world = new Vector2(Camera.main.ScreenToWorldPoint(mousePositionScreen).x, Camera.main.ScreenToWorldPoint(mousePositionScreen).y);
        //rb.position = in_world;

        Vector2 moveDirection = (in_world - rb.position).normalized;

        if (Vector2.Distance(transform.position, in_world) > error)
        {
            rb.velocity = moveDirection * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
