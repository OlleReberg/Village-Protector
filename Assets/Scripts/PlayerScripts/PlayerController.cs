using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement & Rotation")]
    [SerializeField] private float moveSpeed = 5; // Speed at which the player moves
    [SerializeField] private float rotationSpeed = 500f; // Speed at which the player rotates
    [SerializeField] private float jumpPower; // Power of the player's jump

    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius of the sphere used to check for ground
    [SerializeField] private Vector3 groundCheckOffset; // Offset of the sphere used to check for ground
    [SerializeField] private LayerMask groundLayer; // Layermask for the ground objects

    private bool isGrounded; // Flag indicating if the player is grounded
    private float ySpeed; // Vertical speed of the player

    private Quaternion targetRotation; // The rotation that the player should be facing
    private Animator animator; // Animator component of the player
    private CameraController cameraController; // Camera controller component of the main camera
    private CharacterController characterController; // Character controller component of the player

    private void Awake()
    {
        // Get the necessary components
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Get input values
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        // Calculate the move amount
        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        var moveDir = MovePlayer(h, v);
        Vector3 velocity = moveDir * moveAmount;
        // Check for ground
        GroundCheck();

        Gravity();

        // Set the vertical velocity
        velocity.y = ySpeed;

        // If the player is moving, rotate the player to face the move direction
        if (moveAmount > 0)
        {
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        // Rotate the player towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation,
            targetRotation, rotationSpeed * Time.deltaTime);

        // Set the move amount parameter of the animator
        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("isJumping", true); // Set the "isJumping" parameter in the animator to true
            Jump(); // Call the Jump() method to perform the jump
            Debug.Log("Jumping"); // Output a debug message indicating that the player is jumping
        }
        else
        {
            animator.SetBool("isJumping", false); // Set the "isJumping" parameter in the animator to false
        }


        // Move the player
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Gravity()
    {
        // If the player is grounded, set the vertical speed to 0f
        if (isGrounded)
        {
            ySpeed = 0f;
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
        }
        // If the player is not grounded, apply gravity
        if (!isGrounded)
        {
            StartCoroutine(Falling());
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }
    }

    private void Jump()
    {
        // Set the vertical speed to the jump power
        ySpeed = jumpPower; 
        // Move the player upwards based on the jump power and time
        characterController.Move(Vector3.up * (jumpPower * Time.deltaTime)); 
    }

    private Vector3 MovePlayer(float h, float v)
    {
        // Calculate the move direction
        var moveInput = new Vector3(h, 0, v).normalized;
        var moveDir = cameraController.PlanarRotation * moveInput;

        // Calculate the velocity
        var velocity = moveDir * moveSpeed;

        // Move the player
        characterController.Move(velocity * Time.deltaTime);
        return moveDir;
    }

    void GroundCheck()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset),
            groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a sphere to show the ground check area in the editor
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

    private IEnumerator Falling()
    {
        //Turn on falling animation if player is falling down and turning off jumping animation if it's being played
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", true);
    }
}

