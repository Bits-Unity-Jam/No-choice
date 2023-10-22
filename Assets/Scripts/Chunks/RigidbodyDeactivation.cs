using Assets.Script.Chunks;
using UnityEngine;

public class RigidbodyDeactivation : MonoBehaviour, IDeactivateable
{
    [SerializeField]
    private Rigidbody2D _rb2D;

    [SerializeField]
    private GameObject rootGameObject;

    public void Deactivate()
    {
        _rb2D.isKinematic = true;
        _rb2D.velocity = Vector3.zero;

        rootGameObject.SetActive(false);
    }
}