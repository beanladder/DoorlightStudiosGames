using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    public float currentSpeed = 0f;
    [SerializeField] float walkSpeed = 3f;
    public float speed;
    [SerializeField] public float StandingHeight;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float accleration = 0f;
    [SerializeField] float decleration = 0f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;
    Vector3 velocity;
    Vector3 dir;
    [SerializeField] bool isSprinting;

    // Mouse sensitivity for rotating the player
    [SerializeField] float mouseSensitivity = 100f;

    // Threshold angle in degrees
    [SerializeField] float thresholdAngle = 45f;
    bool isMousePastThreshold = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        StandingHeight = controller.height;
    }

    void Update()
    {
        Move();
        RotatePlayer();
        Gravity();

        //check for running
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
        //check for crouching

    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        dir = (transform.forward * vertical + transform.right * horizontal).normalized;
        currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
        if (dir.magnitude > 0.1f && speed <= currentSpeed)
        {
            speed += Time.deltaTime * accleration;
        }
        if (dir.magnitude < 0.1f && speed > 0f)
        {
            speed -= Time.deltaTime * decleration;
        }
        if (isSprinting && speed > currentSpeed)
        {
            speed = currentSpeed;
        }
        else if (!isSprinting && speed > currentSpeed)
        {
            speed -= Time.deltaTime * decleration;
        }

        controller.Move(dir * speed * Time.deltaTime);
    }

    void RotatePlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 playerToMouse = hit.point - transform.position;
            playerToMouse.y = 0f; // Keep the rotation only in the horizontal plane
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            
            // Check if the angle between player's forward direction and player-to-mouse vector is beyond the threshold
            float angle = Vector3.Angle(transform.forward, playerToMouse);
            if (angle > thresholdAngle)
            {
                isMousePastThreshold = true;
            }
            else
            {
                isMousePastThreshold = false;
            }

            // If the mouse has crossed the threshold, rotate the player
            if (isMousePastThreshold)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * mouseSensitivity);
            }
        }
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask))
        {
            return true;
        }
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0f)
        {
            velocity.y = -2f;
        }
        controller.Move(velocity * Time.deltaTime);
    }
}
