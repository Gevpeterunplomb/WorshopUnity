using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class player_controller : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 5f;      // vitesse MARCHE
    public float runSpeed = 10f;      // vitesse COURSE
    public float accelerationTime = 1f; // durée de transition entre marche et course
    public float jumpPower = 8f;      // puissance SAUT
    public float gravity = 15f;       // gravité
    public float lookSpeed = 2f;
    public float lookYLimit = 80f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0f;
    private bool canMove = true;

    private float currentSpeed;       
    private float speedVelocity;      

    // Fov
    public float normalFOV = 60f;
    public float runFOV = 70f;
    public float fovChangeSpeed = 5f;
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
        // Mouvement
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = isRunning ? runSpeed : walkSpeed;

        // Smooth Course
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

        // Caméra
        if (canMove)
        {
            // Rotation Y 
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookYLimit, lookYLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            // Rotation X 
            float mouseY = Input.GetAxis("Mouse X") * lookSpeed;
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + mouseY, 0);
        }
        
        // Fov
        float targetFOV = isRunning ? runFOV : normalFOV;
        currentFOV = Mathf.Lerp(currentFOV, targetFOV, Time.deltaTime * fovChangeSpeed);
        playerCamera.fieldOfView = currentFOV;
    }
}