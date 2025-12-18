using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [Header("Movement")]
    public float walkSpeed = 8f;
    public float sprintSpeed = 11f;
    public float gravity = -19f;
    public float jumpHeight = 3f;

    [Header("Stamina")]
    public float maxStamina = 5f;
    public float staminaDrain = 1.5f;
    public float staminaRegen = 1f;
    public float staminaCooldown = 1.5f;

    private float stamina;
    private float staminaCooldownTimer;

    Vector3 velocity;
    bool isGrounded;
    bool canSprint = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        stamina = maxStamina;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool wantsSprint = Input.GetKey(KeyCode.LeftShift);
        bool isMovingForward = z > 0;

        float currentSpeed = walkSpeed;

        // Sprint logic
        if (wantsSprint && canSprint && stamina > 0 && isMovingForward)
        {
            currentSpeed = sprintSpeed;
            stamina -= staminaDrain * Time.deltaTime;

            if (stamina <= 0)
            {
                stamina = 0;
                canSprint = false;
                staminaCooldownTimer = staminaCooldown;
            }
        }
        else
        {
            // Regenerate stamina
            if (stamina < maxStamina)
                stamina += staminaRegen * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        // Cooldown before sprint allowed again
        if (!canSprint)
        {
            staminaCooldownTimer -= Time.deltaTime;
            if (staminaCooldownTimer <= 0f)
                canSprint = true;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Anti wall-climb
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && hit.normal.y < 0.1f)
            velocity.y = -2f;
    }
}
