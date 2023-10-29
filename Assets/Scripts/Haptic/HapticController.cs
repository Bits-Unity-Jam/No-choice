using System;
using Lofelt.NiceVibrations;
using UnityEngine;

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

    public static HapticController Instance;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayHaptic(HapticType type)
    {
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
