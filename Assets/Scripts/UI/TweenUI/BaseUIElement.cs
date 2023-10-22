using UnityEngine;

namespace Game.UI.Popups
{
    public class BaseUIElement : MonoBehaviour
    {
        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }
        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
