using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    CharacterController controller;

    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float sprintSpeed = 10.0f;
    [SerializeField] float jumpForce = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.05f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    void Start()
    {
        //get character controller
        controller = GetComponent<CharacterController>();

        //disable cursor
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook()
    {
        //calculate cursor pos
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //smooth rot
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        //calculate camera y rot
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90, 90);

        //apply rot
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        //detect inputs
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && controller.isGrounded;

        //smooth mov
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        //prevent player from shaking on objects edges
        if (!controller.isGrounded)
        {
            controller.slopeLimit = 0.0f;
            controller.stepOffset = 0.0f;
        }
        else
        {
            controller.slopeLimit = 45.0f;
            controller.stepOffset = 0.3f;
        }

        //set y velocity to 0 when grounded
        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }
        //apply jump velocity
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocityY = jumpForce;
        }
        //apply gravity velocity
        else
        {
            velocityY += gravity * Time.deltaTime;
        }

        //calculate velocity
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * (isRunning ? sprintSpeed : walkSpeed) + Vector3.up * velocityY;

        //apply mov
        controller.Move(velocity * Time.deltaTime);
    }
}