using UnityEngine;
using UnityEngine.UI;
public class RobotMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;

    private bool isMoving = false;
    private bool isRotating = false;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    public void MoveTo(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }
    public void RotateTowards(Quaternion target)
    {
        targetRotation = target;
        isRotating = true;
    }
    public bool IsMoving()
    {
        return isMoving;
    }

    public bool IsRotating()
    {
        return isRotating;
    }

    void Update()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                transform.rotation = targetRotation;
                isRotating = false;
            }
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }
}