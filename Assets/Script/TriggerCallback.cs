using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCallback : MonoBehaviour
{
    [SerializeField] private string tag;

    [SerializeField] private UnityEvent triggerEnter;
    [SerializeField] private UnityEvent trigerExit;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag== tag )
        {
            triggerEnter.Invoke();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == tag )
        {
            trigerExit.Invoke();
        }
    }
}
