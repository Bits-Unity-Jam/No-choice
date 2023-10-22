using System.Collections;
using UnityEngine;

namespace Mechanics.Effects
{
    public class BackToParentBehaviour : MonoBehaviour
    {
        private Transform _initialParent;
        private Vector3 _initialPosition;

        #region Initialization

        private void Awake()
        {
            _initialParent = transform.parent;
            _initialPosition = transform.localPosition;
        }

        #endregion

        public void BackToParent(float delayInSeconds = 3.5f)
        {
            if (isActiveAndEnabled)
            {
                StartCoroutine(Back(delayInSeconds));
            }
            else
            {
                Back();
            }
        }

        private IEnumerator Back(float seconds, bool isActiveAfterBack = false)
        {
            yield return new WaitForSeconds(seconds);
            Back(isActiveAfterBack);
        }

        private void Back(bool isActiveAfterBack = false)
        {
            gameObject.SetActive(isActiveAfterBack);
            transform.parent = _initialParent;
            transform.localPosition = _initialPosition;
        }
    }
}