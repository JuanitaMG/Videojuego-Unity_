using UnityEngine;

public class PlayerMovementX : MonoBehaviour
{
    [Header("Movimiento")]
    public float speedPlayer = 6f;
    public float jumpForce = 8f;

    [Header("Gravedad")]
    public float gravity = -20f;
    private float velocityY;

    private Vector3 moveDir;

    [Header("Referencias")]
    public CharacterController controller;
    public Animator anim;
    public Camera mainCamera;

    private Vector3 camForward;
    private Vector3 camRight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        CameraDirection();
        HandleMovement();
        HandleRotation();
        HandleGravity();
        HandleJump();
        ApplyMovement();
        UpdateAnimator();
    }

    void HandleInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveDir = new Vector3(h, 0, v).normalized;
    }

    void CameraDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();
    }

    void HandleMovement()
    {
        // Movimiento basado en cámara
        Vector3 targetDir = moveDir.x * camRight + moveDir.z * camForward;
        moveDir = targetDir * speedPlayer;
    }

    void HandleRotation()
    {
        Vector3 flatMovement = new Vector3(moveDir.x, 0, moveDir.z);

        if (flatMovement.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(flatMovement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }
    }

    void HandleGravity()
    {
        if (controller.isGrounded)
        {
            if (velocityY < 0)
                velocityY = -2f;  // Esto evita vibraciones
        }
        else
        {
            velocityY += gravity * Time.deltaTime;
        }
    }

    void HandleJump()
    {
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = jumpForce;
        }
    }

    void ApplyMovement()
    {
        Vector3 finalMove = moveDir;
        finalMove.y = velocityY;

        controller.Move(finalMove * Time.deltaTime);
    }

    void UpdateAnimator()
    {
        float currentSpeed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        float normalized = currentSpeed / speedPlayer;
        anim.SetFloat("Speed", normalized);
    }
}
