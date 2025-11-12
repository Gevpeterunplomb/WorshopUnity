using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class player_controller : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 5f;      
    public float runSpeed = 10f;      
    public float accelerationTime = 1f;
    public float jumpPower = 8f;      
    public float gravity = 15f;       
    public float lookSpeed = 2f;
    public float lookYLimit = 80f;

    [HideInInspector] public bool canMove = true; // Contrôle mouvement externe

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0f;

    private float currentSpeed;
    private float speedVelocity;

    // FOV
    public float normalFOV = 60f;
    public float runFOV = 70f;
    public float fovChangeSpeed = 7f;
    private float currentFOV;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentSpeed = walkSpeed;

        // Initialise le FOV
        currentFOV = normalFOV;
        playerCamera.fieldOfView = currentFOV;
    }

    void Update()
    {
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;
        
        if (!canMove)
        {
            characterController.Move(moveDirection * Time.deltaTime);
            
            if (characterController.isGrounded)
                moveDirection = Vector3.zero;

            return; // Bloque tout
        }

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = isRunning ? runSpeed : walkSpeed;

        // Smooth transition
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, accelerationTime);

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        float moveY = moveDirection.y;
        moveDirection = (forward * inputZ + right * inputX) * currentSpeed;
        moveDirection.y = moveY;

        // Saut
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        // Rotation caméra
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookYLimit, lookYLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        float mouseY = Input.GetAxis("Mouse X") * lookSpeed;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + mouseY, 0);

        // FOV
        float targetFOV = isRunning ? runFOV : normalFOV;
        currentFOV = Mathf.Lerp(currentFOV, targetFOV, Time.deltaTime * fovChangeSpeed);
        playerCamera.fieldOfView = currentFOV;
    }
}
