using System;
using DataStorage;
using Lofelt.NiceVibrations;
using UnityEngine;
using Zenject;

namespace Haptic
{
    public struct HapticSettingsValue
    {
        public bool isEnabled;
    }
    
    public class HapticController : MonoBehaviour
    {
        [Header("Haptic Settings")]
        [SerializeField]
        private HapticPatterns.PresetType startHaptic;
        [SerializeField]
        private HapticPatterns.PresetType endHaptic;
        [SerializeField]
        private HapticPatterns.PresetType collectHaptic;
        [SerializeField]
        private HapticPatterns.PresetType reflectHaptic;

        [SerializeField] private bool _isHapticActive;
        public static HapticController Instance;

        private IStorage _storage;
        private ISerializer _serializer;
        private static bool DefaultValue => true;
        
        [Inject]
        private void Construct(IStorage storage, ISerializer serializer)
        {
            _storage = storage;
            _serializer = serializer;
        }
        
        void Awake()
        {
            LoadSavedData();
            
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void LoadSavedData()
        {
            string savedSettings;

            HapticSettingsValue deserealized;
            try
            {
                savedSettings = _storage.Load($"/Resources/Settings/Haptic");
                deserealized = _serializer.Deserialize<HapticSettingsValue>(savedSettings);
            }
            catch (Exception e)
            {
                SaveSettings(DefaultValue);
                return;
            }

            _isHapticActive = deserealized.isEnabled;
        }

        private void SaveSettings(bool value)
        {
            string serialized = _serializer.Serialize(new HapticSettingsValue() { isEnabled = value });
            _storage.SaveAs(serialized, $"/Resources/Settings/Haptic");
        }
        
        public void SetHapticActiveState(bool isActive) => _isHapticActive = isActive;
        public bool GetHapticActiveState() => _isHapticActive;
        public bool ChangeHapticActiveState() => _isHapticActive = !_isHapticActive;

        public void PlayHaptic(HapticType type)
        {
            if (!_isHapticActive) return;
            
            switch (type)
            {
                case HapticType.Start:
                    HapticPatterns.PlayPreset(startHaptic);
                    break;
                case HapticType.End:
                    HapticPatterns.PlayPreset(endHaptic);
                    break;
                case HapticType.Collect:
                    HapticPatterns.PlayPreset(collectHaptic);
                    break;
                case HapticType.Reflect:
                    HapticPatterns.PlayPreset(reflectHaptic);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public enum HapticType
    {
        Start,
        End,
        Collect,
        Reflect
    }
}