using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Currencies;
using Data;
using DataStorage;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace Shops
{
    public struct PurchasedShieldsSaveData
    {
        public int[] purchasedShieldIds;
    }

    public class ShieldShop : MonoBehaviour
    {
        private ShieldDatabase _shieldDb;
        [SerializeField] private ShieldData selectedShield;
        [SerializeField] private TMP_Text shieldPriceText;
        [SerializeField] private Transform shieldPriceTextParentTransform;
        [SerializeField] private Button buyButton;
        [SerializeField] private GameObject shieldSlotPrefab;
        [SerializeField] private Transform shieldSlotParentTransform;
        [SerializeField] private Image selectedShieldImage;

        private const string kPurchasedShieldsSavedataPath = "/Resources/SaveData/Shields/Purchased";

        private ISerializer _serializer;
        private IStorage _storage;
        private CurrencyService _currencyService;
        private List<ShieldShopSlot> _createdSlots = new();

        [Inject]
        private void Construct(ShieldDatabase shieldDb, ISerializer serializer, IStorage storage,
            CurrencyService currencyService)
        {
            _shieldDb = shieldDb;
            _serializer = serializer;
            _storage = storage;
            _currencyService = currencyService;
        }

        private void Start()
        {
            InitializeShop();
        }

        public async void ShowShieldData(ShieldData shieldData)
        {
            shieldPriceText.text = shieldData.ShieldPrice.ToString();
            
            {
                shieldPriceText.transform.parent.parent.gameObject.SetActive(shieldData.ShieldPrice != 0);
                buyButton.gameObject.SetActive(shieldData.ShieldPrice != 0);
            }

            AsyncOperationHandle<Sprite> loadedObject =
                Addressables.LoadAssetAsync<Sprite>(shieldData.ShieldIconPath);
            await loadedObject.Task;

            selectedShieldImage.sprite = loadedObject.Result;
        }

        private async void InitializeShop()
        {
            _createdSlots = new();
            PurchasedShieldsSaveData purchasedShieldData = LoadPurchasedShieldData();

            foreach (Transform tr in shieldSlotParentTransform)
            {
                Destroy(tr);
            }

            var shieldTypescount = Enum.GetNames(typeof(ShieldID)).Length;

            for (int i = 0; i < shieldTypescount; i++)
            {
                var created = await CreateShieldSlot(
                    _shieldDb.GetItemWithId((ShieldID)i),
                    purchasedShieldData.purchasedShieldIds.Contains(i));

                _createdSlots.Add(created);
            }

            _createdSlots.ForEach(slot => slot.OnSlotButtonClick += HandleSlotClicked);
        }

        private void HandleSlotClicked(ShieldData obj)
        {
            selectedShield = obj;
            ShowShieldData(obj);
        }


        private void HandlePurchase()
        {
        }

        private PurchasedShieldsSaveData LoadPurchasedShieldData()
        {
            string loadedSaveData = _storage.Load(kPurchasedShieldsSavedataPath);

            PurchasedShieldsSaveData deserealized = _serializer.Deserialize<PurchasedShieldsSaveData>(loadedSaveData);
            if (deserealized.purchasedShieldIds == default)
            {
                deserealized.purchasedShieldIds = Array.Empty<int>();
            }

            return deserealized;
        }

        private async Task<ShieldShopSlot> CreateShieldSlot(ShieldData itemWithId, bool isPurchased)
        {
            var created = Instantiate(shieldSlotPrefab, shieldSlotParentTransform);
            var slot = created.GetComponent<ShieldShopSlot>();

            return await slot.Initialize(itemWithId, isPurchased, _currencyService.CanSpend(itemWithId.ShieldPrice));
        }
    }
}