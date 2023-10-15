using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        audioSource.Play();
    }
}
