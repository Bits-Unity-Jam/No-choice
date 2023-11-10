using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer audioMixer;
        [SerializeField]
        private Slider slider;

        [SerializeField]
        private string exposedParam = "MasterVolume";

        private const float kMinVolume = -80f;
        public const float kMaxVolume = 0f;
        private void Start()
        {
            slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        }

        public void OnSliderValueChanged()
        {
            float sliderValue = slider.value;
            float volume = Mathf.Lerp(kMinVolume, kMaxVolume, sliderValue);
            audioMixer.SetFloat(exposedParam, volume);
        }
    }
}