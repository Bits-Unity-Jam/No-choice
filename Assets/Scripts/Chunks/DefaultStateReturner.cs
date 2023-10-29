using UnityEngine;
using UnityEngine.Serialization;

namespace Chunks
{
    public class DefaultStateReturner : MonoBehaviour, IDefaultStateReturner
    {
        [SerializeField]
        private Rigidbody2D _rb2D;

        [FormerlySerializedAs("rootGameObject")] [SerializeField]
        private GameObject _rootGameObject;

        [Space, Header("Default State Properties:")] [SerializeField]
        private bool isActive;
        
        [SerializeField]
        private bool isKinematicRb2D;
        
        public void ReturnToDefaultState()
        {
            _rootGameObject.SetActive(isActive);
            _rb2D.isKinematic = isKinematicRb2D;
            _rb2D.velocity = Vector3.zero;
        }
    }
}