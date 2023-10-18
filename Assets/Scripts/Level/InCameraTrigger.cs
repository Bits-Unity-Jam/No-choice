using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InCameraTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent inCamera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="MainCamera")
        {
            inCamera.Invoke();
        }
    }
}
