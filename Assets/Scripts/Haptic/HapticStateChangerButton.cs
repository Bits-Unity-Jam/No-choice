using UnityEngine;
using UnityEngine.UI;

namespace Haptic
{
    public class HapticStateChangerButton: MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Sprite enabledSprite;
        [SerializeField] private Sprite disabledSprite;
        [SerializeField] Image buttonImage;
        private HapticController _hapticController;
        
        private void Start()
        {
            _hapticController = HapticController.Instance;
            
            buttonImage.sprite = _hapticController.GetHapticActiveState() ? enabledSprite : disabledSprite;
            
            _button.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            _hapticController.ChangeHapticActiveState();
            _hapticController.PlayHaptic(HapticType.LightImpact);
            buttonImage.sprite = _hapticController.GetHapticActiveState() ? enabledSprite : disabledSprite;
        }
    }
}