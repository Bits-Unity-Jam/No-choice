using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerCatcher : MonoBehaviour, IPointerDownHandler
{

    public event Action OnPointerDownCaught;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownCaught?.Invoke();
    }

}
