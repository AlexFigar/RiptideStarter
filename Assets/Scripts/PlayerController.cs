using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float rotationSmoothTime = 0.12f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundCheckDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Vector2 movementInput;
    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;
    private float rotationVelocity;
    private float targetRotationY;

    // Input Actions
    private InputAction moveAction;
    private InputAction runAction;
    private InputAction jumpAction;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Get references to input actions
        moveAction = InputSystem.actions["Move"];
        runAction = InputSystem.actions["Run"];
        jumpAction = InputSystem.actions["Jump"];

        // Enable input actions
        moveAction.Enable();
        runAction.Enable();
        jumpAction.Enable();

        // Set initial speed
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJump();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        movementInput = moveAction.ReadValue<Vector2>();

        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);
        float targetSpeed = (runAction.IsPressed() ? runSpeed : walkSpeed) * movementInput.magnitude;

        // Smoothly change speed
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 5f);

        if (moveDirection != Vector3.zero)
        {
            targetRotationY = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + transform.eulerAngles.y;
            controller.Move(transform.forward * currentSpeed * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        if (movementInput != Vector2.zero)
        {
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotationY, ref rotationVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        }
    }

    private void HandleJump()
    {
        isGrounded = Physics.CheckSphere(transform.position + Vector3.down * groundCheckDistance, 0.1f, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small force to keep player grounded
        }

        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Optional: Add this method to handle input action events if needed
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}
