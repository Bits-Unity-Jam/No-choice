using UnityEngine;

namespace Currencies
{
    public class CurrencyService : MonoBehaviour
    {
        [SerializeField] private int _currency;
        
        private const string PATH = "SaveData/Currency/Coins/";

        public int Currency => PlayerPrefs.GetInt(PATH, 0);

        public void Increase(int currency)
        {
            _currency += currency;
            SaveCurrencyData();
        }

        public void Decrease(int currency)
        {
            if (currency > _currency)
            {
                return;
            }
            
            _currency += currency;
            SaveCurrencyData();
        }
        
        public bool CanSpend(int currency)
        {
            return !(currency > _currency);
        }

        private void SaveCurrencyData()
        {
            PlayerPrefs.SetInt(PATH, _currency);
        }
    }
}
