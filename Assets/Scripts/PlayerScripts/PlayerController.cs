using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement & Rotation")]
    [SerializeField] private float moveSpeed = 5; // Speed at which the player moves
    [SerializeField] private float rotationSpeed = 500f; // Speed at which the player rotates
    [SerializeField] private float jumpPower; // Force applied when the player jumps

    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius for ground check sphere
    [SerializeField] private Vector3 groundCheckOffset; // Offset for ground check sphere
    [SerializeField] private LayerMask groundLayer; // Layer mask for ground detection

    private bool isGrounded; // Flag to check if the player is grounded
    private float ySpeed; // Vertical speed for gravity and jumping
    private Quaternion targetRotation; // Target rotation for the player
    private Animator animator; // Animator component for controlling animations
    private CameraController cameraController; // Reference to the camera controller
    private CharacterController characterController; // Reference to the character controller
    private PlayerCombatController playerCombatController; // Reference to the combat controller
    private PlayerState playerState = PlayerState.Idle; // Current state of the player

    private void Awake()
    {
        // Get references to necessary components
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerCombatController = GetComponent<PlayerCombatController>();

        // Subscribe to attack events
        playerCombatController.OnAttackStart += HandleAttackStart;
        playerCombatController.OnAttackEnd += HandleAttackEnd;
    }

    private void Update()
    {
        Debug.Log($"PlayerState: {playerState}");
        
        // Handle player actions based on the current state
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Moving:
                HandleMovement();
                break;
            case PlayerState.Attacking:
                // Prevent movement while attacking
                break;
            case PlayerState.Jumping:
                // Handle jump logic if needed
                Gravity();
                GroundCheck();
                break;
        }
    }

    private void HandleMovement()
    {
        // Get input for horizontal and vertical movement
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v)); // Calculate movement amount
        var moveDir = MovePlayer(h, v); // Calculate movement direction
        Vector3 velocity = moveDir * moveAmount; // Calculate velocity based on movement direction and amount

        GroundCheck(); // Check if the player is grounded
        Gravity(); // Apply gravity
        velocity.y = ySpeed; // Apply vertical speed

        // Rotate the player towards the movement direction
        if (moveAmount > 0)
        {
            targetRotation = Quaternion.LookRotation(moveDir);
            playerState = PlayerState.Moving; // Set player state to moving
        }
        else
        {
            playerState = PlayerState.Idle; // Set player state to idle
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Smoothly rotate the player
        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime); // Update animator with movement amount

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerState = PlayerState.Jumping; // Set player state to jumping
            animator.SetBool("isJumping", true); // Set jumping animation
            Jump(); // Perform jump
        }
        else
        {
            animator.SetBool("isJumping", false); // Reset jumping animation
        }
        characterController.Move(velocity * Time.deltaTime); // Move the player
    }

    private void Gravity()
    {
        // Apply gravity to the player
        if (isGrounded)
        {
            ySpeed = 0f; // Reset vertical speed when grounded
            animator.SetBool("isFalling", false); // Reset falling animation
            animator.SetBool("isJumping", false); // Reset jumping animation
        }
        else
        {
            StartCoroutine(Falling()); // Start falling coroutine
            ySpeed += Physics.gravity.y * Time.deltaTime; // Apply gravity to vertical speed
        }
    }

    private void Jump()
    {
        ySpeed = jumpPower; // Apply jump power to vertical speed
        characterController.Move(Vector3.up * (jumpPower * Time.deltaTime)); // Move the player upwards
    }

    private Vector3 MovePlayer(float h, float v)
    {
        // Calculate movement direction based on input and camera rotation
        var moveInput = new Vector3(h, 0, v).normalized;
        var moveDir = cameraController.PlanarRotation * moveInput;
        var velocity = moveDir * moveSpeed; // Calculate velocity
        characterController.Move(velocity * Time.deltaTime); // Move the player
        return moveDir; // Return movement direction
    }

    private void GroundCheck()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw ground check sphere in the editor
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

    private IEnumerator Falling()
    {
        // Handle falling logic with a short delay
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isJumping", false); // Reset jumping animation
        animator.SetBool("isFalling", true); // Set falling animation
    }

    private void HandleAttackStart()
    {
        // Set player state to attacking when attack starts
        playerState = PlayerState.Attacking;
        Debug.Log("Attack Started");
    }

    public void HandleAttackEnd()
    {
        Debug.Log("Attack Ended");
        //Reset player state to movement state when attack ends
        playerState = PlayerState.Idle;
        
    }
}



