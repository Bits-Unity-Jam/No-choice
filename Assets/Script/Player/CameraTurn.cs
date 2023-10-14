using UnityEngine;
using DG.Tweening;

public class CameraTurn : MonoBehaviour
{ 
    public float rotationDuration = 2.0f;

    public void Turn()
    {
        transform.DORotate(new Vector3(0f, 0.0f, transform.rotation.eulerAngles.z != 180.0f ? 180.0f: 0.0f), rotationDuration).SetEase(Ease.Linear);
    }
}