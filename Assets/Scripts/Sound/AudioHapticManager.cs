using UnityEngine;
using System.Collections;

public class AudioHapticManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [Header("Settings")]
    [SerializeField]
    private bool isActiveMusic;
    [SerializeField]
    private bool isActiveHaptic;
    [SerializeField]
    private HapticType hapticType;
    
    [Header("Delay")]
    [SerializeField]
    private float delayBetweenActions;

    [Header("Tag Filter")]
    [SerializeField]
    private bool useTagFilter;
    [SerializeField]
    private string objectTag;

    private bool isPlaying = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isPlaying && (!useTagFilter || other.gameObject.CompareTag(objectTag)))
        {
            StartCoroutine(PlayAudioHapticWithDelay());
        }
    }

    private IEnumerator PlayAudioHapticWithDelay()
    {
        isPlaying = true;

        if (isActiveMusic)
        {
            audioSource.Play();
        }

        yield return new WaitForSeconds(delayBetweenActions);

        if (isActiveHaptic)
        {
            HapticController.Instance.PlayHaptic(hapticType);
        }

        isPlaying = false;
    }
}