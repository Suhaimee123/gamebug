using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maximumSpeed = 10f;
    public float rotationSpeed = 720f;

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        // Check if the player is running
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude *= 2;
        }

        animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);

        float speed = inputMagnitude * maximumSpeed;
        movementDirection.Normalize();

        // Apply gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // Ensure the character stays grounded
        if (characterController.isGrounded)
        {
            ySpeed = -0.5f; // Apply a small downward force to keep the character grounded
        }

        Vector3 velocity = movementDirection * speed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
}
