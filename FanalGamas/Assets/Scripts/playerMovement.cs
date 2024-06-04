using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Animator anim;

    private float inputX;
    private float inputY;
    private Vector3 velocity;

    private void Update()
    {
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

        // Move the character controller
        controller.Move(move);

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
