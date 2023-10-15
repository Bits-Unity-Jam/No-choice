using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _defaultSpeed = 2.5f;

    public float Speed { get => _speed; set => _speed = value; }
    public float DefaultSpeed { get => _defaultSpeed; private set => _defaultSpeed = value; }

   

    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * Speed);
    }
}
