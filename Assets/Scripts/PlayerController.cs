using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool canMove;

    [SerializeField] Transform playerCamera;
    CharacterController controller;

    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float sprintSpeed = 10.0f;
    [SerializeField] float jumpForce = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] float pushPower = 2.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.05f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    float currentHorizontalSpeed;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    int horizontalAxis;
    int verticalAxis;

    public static PlayerController instance;

    void Awake()
    {
        //create instance for the script
        if (instance != null)
        {
            Debug.LogWarning("There is multiple PlayerController instances!");
            return;
        }

        instance = this;
    }

    void Start()
    {
        canMove = true;

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
        if (canMove)
        {
            UpdateMouseLook();
        }

        UpdateMovement();

        //get current horizontal speed
        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        currentHorizontalSpeed = horizontalVelocity.magnitude;

        //apply keybinds on axis
        //WASD
        if (SettingsMenuManager.instance.keybindsProfile == 0)
        {
            verticalAxis = (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0);
            horizontalAxis = (Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.A) ? -1 : 0);
        }
        //ZQSD
        else if (SettingsMenuManager.instance.keybindsProfile == 1)
        {
            verticalAxis = (Input.GetKey(KeyCode.Z) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0);
            horizontalAxis = (Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.Q) ? -1 : 0);
        }
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
        Vector2 targetDir = new Vector2(horizontalAxis, verticalAxis);
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
        if (Input.GetButtonDown("Jump") && controller.isGrounded && canMove)
        {
            velocityY = jumpForce;
        }
        //apply gravity velocity
        else
        {
            velocityY += gravity * Time.deltaTime;
        }

        //calculate velocity
        Vector3 velocity = (canMove ? (transform.forward * currentDir.y + transform.right * currentDir.x) : Vector3.zero) * (isRunning ? sprintSpeed : walkSpeed) + Vector3.up * velocityY;

        //apply mov
        controller.Move(velocity * Time.deltaTime);
    }

    //push rigidbodies with player's body
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        //no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        //don't push object bellow the player
        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        //calculate push direction (only push objects to the sides, never up and down)
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        //apply push
        body.velocity = pushDir * pushPower * currentHorizontalSpeed;
    }
}
