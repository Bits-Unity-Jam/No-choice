using System;
using System.Threading.Tasks;
using Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Shops
{
    public class ShieldShopSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private Image shieldIcon;
        [SerializeField] private Image darkCoverImage;
        [SerializeField] private Button slotButton;
        [SerializeField] private ShieldData shieldData;

        [SerializeField]
        private Color canSpendtextColor;
        [SerializeField]
        private Color cantSpendtextColor;

        [SerializeField]
        private bool _isPurchased;

        public event Action<ShieldData> OnSlotButtonClick;
        
        public bool IsPurchased
        {
            get { return _isPurchased; }
            set
            {
                _isPurchased = value;
                darkCoverImage.enabled = !_isPurchased;
            }
        }

        public Button SlotButton => slotButton;

        private void Start()
        {
            slotButton.onClick.AddListener(HandleSlotClick);
        }

        private void HandleSlotClick()
        {
            OnSlotButtonClick?.Invoke(shieldData);
        }

        public async Task<ShieldShopSlot> Initialize(ShieldData itemWithId, bool isPurchased, bool canPurchase)
        {
            shieldData = itemWithId;
            AsyncOperationHandle<Sprite> loadedObject = 
                Addressables.LoadAssetAsync<Sprite>(itemWithId.ShieldIconPath);
            await loadedObject.Task;
            
            shieldIcon.sprite = loadedObject.Result;
            
            if (itemWithId.ShieldPrice == 0)
            {
                isPurchased = true;
                priceText.gameObject.SetActive(false);
            }

            IsPurchased = isPurchased;
            priceText.text = itemWithId.ShieldPrice.ToString();
            priceText.color = canPurchase ? canSpendtextColor : cantSpendtextColor;
            return this;
        }
    }
}