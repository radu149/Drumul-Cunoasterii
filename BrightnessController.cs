using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class BrightnessController : MonoBehaviour
{
    public Volume postProcessingVolume;
    private ColorAdjustments colorAdjustments;

    void Start()
    {
        if (postProcessingVolume.profile.TryGet(out colorAdjustments))
        {
            // Initialized
        }
        else
        {
            Debug.LogError("ColorAdjustments not found in the volume profile!");
        }
    }

    public void SetBrightness(float value)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = value;
        }
    }
}