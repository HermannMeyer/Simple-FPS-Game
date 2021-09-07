using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 5f;
    public float gravity = -10f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        horizontalMovement();
        verticalMovement();
    }

    // Handle movements on the XZ plane
    void horizontalMovement()
    {
        float xMovemnent = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 movement = transform.right * xMovemnent + transform.forward * zMovement;
        controller.Move(movement);
    }

    // Handle movements on the Y axis (falling, jumping)
    void verticalMovement()
    {
        // If the player is touching the ground, resets the fall velocity to -2
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Handle player jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Calculate the velocity based on the formula v = sqrt(h * -2 * g)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


}
