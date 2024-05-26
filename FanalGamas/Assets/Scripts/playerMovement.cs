using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Animator anim;

    private float inputX;
    private float inputY;
    private Vector3 velocity;
    private bool isGrounded;
    private Transform groundCheck;

    private Vector2 boundaryMin;
    private Vector2 boundaryMax;

    private void Awake()
    {
        groundCheck = new GameObject("GroundCheck").transform;
        groundCheck.SetParent(transform);
        groundCheck.localPosition = Vector3.down * 0.5f;

        // Find the plane in the scene
        GameObject plane = GameObject.Find("Plane");
        if (plane != null)
        {
            Collider planeCollider = plane.GetComponent<Collider>();
            if (planeCollider != null)
            {
                // Calculate boundaries based on the collider bounds
                Vector3 planeMin = planeCollider.bounds.min;
                Vector3 planeMax = planeCollider.bounds.max;

                boundaryMin = new Vector2(planeMin.x, planeMin.z);
                boundaryMax = new Vector2(planeMax.x, planeMax.z);
            }
        }
        else
        {
            Debug.LogError("Plane not found in the scene. Please ensure there is a GameObject named 'Plane' with a Collider.");
        }
    }

    private void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input from Horizontal and Vertical axes
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        // Create direction vector based on input and normalize it
        Vector3 direction = new Vector3(inputX, 0f, inputY).normalized;

        // Determine if the player is running by checking if the Shift key is pressed
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Move the character controller based on direction and current speed
        Vector3 move = direction * currentSpeed * Time.deltaTime;

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        move.y = velocity.y * Time.deltaTime;

        // Calculate the new position
        Vector3 newPosition = transform.position + move;

        // Clamp the position within boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, boundaryMin.x, boundaryMax.x);
        newPosition.z = Mathf.Clamp(newPosition.z, boundaryMin.y, boundaryMax.y);

        // Move the character controller based on clamped position
        controller.Move(newPosition - transform.position);

        // Set animator parameters based on movement and running state
        bool isMoving = direction.magnitude > 0.1f;
        anim.SetBool("Walk", isMoving && !isRunning);
        anim.SetBool("Run", isMoving && isRunning);
        anim.SetBool("Moving", isMoving);

        // Rotate the character towards the direction of movement
        if (isMoving)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion toRotation = Quaternion.AngleAxis(angle, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 15f);
        }
    }
}
