using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProportionalScaling : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransfrom;

    private void Start()
    {
        _rectTransfrom.sizeDelta = new Vector2(600, 600);
    }
}

