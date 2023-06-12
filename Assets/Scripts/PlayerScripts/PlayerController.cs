using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement & Rotation")]
    [SerializeField] private float moveSpeed = 5; // Movement speed of the player
    [SerializeField] private float rotationSpeed = 500f; // Rotation speed of the player

    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius of the sphere used to check for ground
    [SerializeField] private Vector3 groundCheckOffset; // Offset of the sphere used to check for ground
    [SerializeField] private LayerMask groundLayer; // Layermask for the ground objects

    private bool _isGrounded; // Stores if the player is grounded
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
        // Get input values
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        // Calculate the move amount
        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

        // Calculate the move direction
        var moveInput = new Vector3(h, 0, v).normalized;
        var moveDir = cameraController.PlanarRotation * moveInput;

        // Calculate the velocity
        var velocity = moveDir * moveSpeed;

        // Check for ground
        GroundCheck();

        // If the player is grounded, set the vertical speed to -0.5f
        if (_isGrounded)
        {
            ySpeed = -0.5f;
        }
        else
        {
            // If the player is not grounded, apply gravity
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }

        // Set the vertical velocity
        velocity.y = ySpeed;

        // Move the player
        characterController.Move(velocity * Time.deltaTime);

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
    }

    void GroundCheck()
    {
        // Check if the player is grounded
        _isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset),
            groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a sphere to show the ground check area in the editor
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }
}
