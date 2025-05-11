using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // Assign the CameraTarget here
    public Vector3 offset = new Vector3(0f, 1.5f, -4f);

    [Header("Settings")]
    public float rotationSpeed = 4f;
    public float minY = -35f;
    public float maxY = 65f;
    public float distance = 5f;
    public float smoothTime = 0.1f;

    private float currentYaw = 0f;
    private float currentPitch = 10f;
    private Vector3 velocity = Vector3.zero;

    [Header("Essentials")]
    public ThirdPersonController ThirdPersonController;

    void LateUpdate()
    {
        if (target == null) return;

        if(ThirdPersonController.OptionsMovementNull)
        {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        currentYaw += mouseX;
        currentPitch -= mouseY;
        currentPitch = Mathf.Clamp(currentPitch, minY, maxY);

        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.LookAt(target);
        }
    }
}
