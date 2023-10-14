using UnityEngine;
using DG.Tweening;

public class CameraTurn : MonoBehaviour
{ 
    public float rotationDuration = 2.0f;

    public void Turn()
    {
        transform.DORotate(new Vector3(0f, 0.0f, 180.0f), rotationDuration).SetEase(Ease.Linear);
        Debug.Log("Turn");
    }
    public void Turn2()
    {
        transform.DORotate(new Vector3(0f, 0.0f, 0.0f), rotationDuration).SetEase(Ease.Linear);
        Debug.Log("Turn2");
    }
}