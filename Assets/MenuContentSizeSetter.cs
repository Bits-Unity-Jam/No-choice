using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MenuContentSizeSetter : MonoBehaviour
{

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform refRectTransform;
    [SerializeField] private float space = 20;

    private void Start()
    {
        UpdateMenuSize();
    }

    [Button]
    private void UpdateMenuSize()
    {
        rectTransform.sizeDelta = new Vector2(refRectTransform.rect.width * 3 + space, refRectTransform.sizeDelta.y);
    }
}
