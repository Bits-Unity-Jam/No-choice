using Assets.Script.Chunks;
using System.ComponentModel;
using UnityEngine;

namespace Assets.Scripts.Chunks
{
    public class RigidbodyActivation : MonoBehaviour, IActivateable
    {
        [SerializeField]
        private Rigidbody2D _rb2D;

        [SerializeField]
        private GameObject rootGameObject;

        public void Activate()
        {
            rootGameObject.SetActive(true);

            _rb2D.isKinematic = false;
            _rb2D.velocity = Vector3.zero;
        }
    }
}