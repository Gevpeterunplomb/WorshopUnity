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

    // 🎯 --- COLLISION CAMÉRA ---
    [Header("Collision Caméra")]
    public Transform cameraPivot;           // Empty derrière la tête du joueur
    public float cameraHeight = 1.7f;       // Hauteur caméra (position Y)
    public float cameraDistance = 3f;       // Distance caméra (position Z)
    public float cameraAngleX = 15f;        // Inclinaison manuelle (rotation X)
    public float minDistance = 0.3f;        // Distance minimale si collision
    public float smoothSpeed = 10f;         // Vitesse retour caméra
    public LayerMask collisionMask;         // Masque des obstacles

    private float currentDistance;          // Distance dynamique actuelle
    // 🎯 --- FIN COLLISION CAMÉRA ---


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentSpeed = walkSpeed;

        // Initialise le FOV
        currentFOV = normalFOV;
        if (playerCamera != null)
            playerCamera.fieldOfView = currentFOV;

        // Initialise la distance actuelle à la valeur manuelle
        currentDistance = cameraDistance;
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
        if (playerCamera != null)
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX + cameraAngleX, 0, 0);

        float mouseY = Input.GetAxis("Mouse X") * lookSpeed;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + mouseY, 0);

        // FOV
        float targetFOV = isRunning ? runFOV : normalFOV;
        currentFOV = Mathf.Lerp(currentFOV, targetFOV, Time.deltaTime * fovChangeSpeed);
        if (playerCamera != null)
            playerCamera.fieldOfView = currentFOV;
    }

    void LateUpdate()
    {
        HandleCameraCollision();
    }

    // 🎥 --- GESTION COLLISION CAMÉRA ---
    void HandleCameraCollision()
    {
        if (playerCamera == null || cameraPivot == null)
            return;

        // Point de départ (hauteur manuelle)
        Vector3 pivotPos = cameraPivot.position + Vector3.up * cameraHeight;

        // Position théorique sans collision (distance manuelle)
        Vector3 desiredPosition = pivotPos - cameraPivot.forward * cameraDistance;

        // Vérifie s'il y a un obstacle entre le pivot et la caméra
        if (Physics.Linecast(pivotPos, desiredPosition, out RaycastHit hit, collisionMask))
        {
            float hitDist = Vector3.Distance(pivotPos, hit.point) - 0.1f;
            currentDistance = Mathf.Clamp(hitDist, minDistance, cameraDistance);
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, cameraDistance, Time.deltaTime * smoothSpeed);
        }

        // Met à jour position caméra
        playerCamera.transform.position = pivotPos - cameraPivot.forward * currentDistance;
    }

    // Gizmos debug
    void OnDrawGizmosSelected()
    {
        if (cameraPivot != null && playerCamera != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(cameraPivot.position, playerCamera.transform.position);
        }
    }
}
