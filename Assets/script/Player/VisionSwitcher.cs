using UnityEngine;
using UnityEngine.Rendering;

public class VisionSwitcher : MonoSingleton<VisionSwitcher>
{
    [Header("Volume")]
    [SerializeField] private Volume globalVolume;

    [Header("Vision Profiles")]
    [SerializeField] private VolumeProfile normalVision;
    [SerializeField] private VolumeProfile maskVision;
    [SerializeField] private VolumeProfile deathVision;

    private void Awake()
    {
        base.Awake();
        if (globalVolume == null)
            globalVolume = GetComponent<Volume>();

        SetNormalVision();
    }

    // ---------------- PUBLIC API ----------------

    public void SetNormalVision()
    {
        SwitchProfile(normalVision);
    }

    public void SetMaskVision()
    {
        SwitchProfile(maskVision);
    }

    public void SetDeathVision()
    {
        SwitchProfile(deathVision);
    }

    // ---------------- INTERNAL ----------------

    private void SwitchProfile(VolumeProfile profile)
    {
        if (profile == null)
        {
            Debug.LogWarning("VisionSwitcher: VolumeProfile is missing");
            return;
        }

        globalVolume.profile = profile;
    }
}
