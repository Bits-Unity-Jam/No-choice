using System;
using System.Globalization;
using DataStorage;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace Audio
{
    public struct SoundSettingsValue
    {
        public float value;
    }
    public class AudioController : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer audioMixer;
        [SerializeField]
        private Slider slider;

        [SerializeField]
        private string exposedParam = "MasterVolume";

        private IStorage _storage;
        private ISerializer _serializer;

        private const float kMinVolume = -80f;
        public const float kMaxVolume = 0f;

        private static float DefaultValue => 0.7f;
        
        [Inject]
        private void Construct(IStorage storage, ISerializer serializer)
        {
            _storage = storage;
            _serializer = serializer;
        }
        
        private void Start()
        {
            slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
            string savedSettings;
            SoundSettingsValue deserealized;
            try
            {
                savedSettings = _storage.Load($"/Resources/SoundSettings/{exposedParam}");
                deserealized = _serializer.Deserialize<SoundSettingsValue>(savedSettings);
            }
            catch (Exception e)
            {
                SaveSettings(DefaultValue);
                slider.value = DefaultValue;
                return;
            }
            slider.value = deserealized.value;
        }


        private void SaveSettings(float value)
        {
            var serialized = _serializer.Serialize(new SoundSettingsValue { value = value });
            _storage.SaveAs(serialized, $"/Resources/SoundSettings/{exposedParam}");
        }

        public void OnSliderValueChanged()
        {
            float sliderValue = slider.value;
            float volume = Mathf.Lerp(kMinVolume, kMaxVolume, sliderValue);
            audioMixer.SetFloat(exposedParam, volume);
            
            SaveSettings(sliderValue);
        }
    }
}