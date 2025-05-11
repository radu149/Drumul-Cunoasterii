using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = 20f;
    public float jumpHeight = 3f;
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    public float SpeedModifier = 5f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private float verticalVelocity;
    private float cameraPitch = 0f;
    private float SpeedDebug = 0f;
//    private float i=0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        SpeedDebug=moveSpeed;
    }

    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            moveDirection = move * moveSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }

            if(Input.GetButtonDown("Debug Multiplier"))
            {
                moveSpeed=moveSpeed+SpeedModifier;
            }

            if(Input.GetButtonUp("Debug Multiplier"))
            {
                moveSpeed=moveSpeed-SpeedModifier;
            }
        }

        

        verticalVelocity -= gravity * Time.deltaTime;
        moveDirection.y = verticalVelocity;

        controller.Move(moveDirection * Time.deltaTime);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }
}
