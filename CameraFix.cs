using UnityEngine;

public class CameraFix : MonoBehaviour
{
    void OnEnable()
    {
        Camera cam = GetComponent<Camera>();
        if (cam != null)
        {
            cam.enabled = false;
            cam.enabled = true;
        }
    }
}
