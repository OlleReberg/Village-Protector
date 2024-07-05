using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class PlayerCombatController : MonoBehaviour
{
    // References to necessary components and data
    private Animator animator;
    private Weapon weapon;
    public List<AttackSO> attackCombo; // List of attack scriptable objects for combo attacks
    private float lastClickedTime; // Time when the last attack was initiated
    private float lastComboEnd; // Time when the last combo ended
    private int comboCounter; // Counter to track the current attack in the combo
    public float delay = 1f; // Delay before executing certain actions
    public VisualEffect lightning; // Visual effect for special abilities
    private PlayerController playerController;

    // Properties and events for attack state
    public bool IsAttacking { get; private set; } // Property to check if the player is attacking
    public event Action OnAttackStart; // Event triggered when an attack starts
    public event Action OnAttackEnd; // Event triggered when an attack ends

    private void Awake()
    {
        // Get references to necessary components
        animator = GetComponent<Animator>();
        lightning.gameObject.SetActive(false); // Disable lightning effect initially
    }

    private void Update()
    {
        // Check for attack input (left mouse button or equivalent)
        if (Input.GetButtonDown("Fire1"))
        {
            Attack(); // Perform attack action
        }

        // Check for ability input (right mouse button or equivalent)
        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(AbilitySequence()); // Perform special ability action
        }
        
        ExitAttack(); // Check and handle the end of an attack
    }

    void Attack()
    {
        // Check if the player can initiate a new combo or continue the current one
        if (Time.time - lastComboEnd > 0.5f && comboCounter < attackCombo.Count)
        {
            CancelInvoke("EndCombo"); // Cancel any pending EndCombo invocations

            // Check if enough time has passed since the last attack click
            if (Time.time - lastClickedTime >= 0.3f)
            {
                IsAttacking = true; // Set attacking state to true
                OnAttackStart?.Invoke(); // Trigger attack start event

                // Set the animator to the current attack animation
                animator.runtimeAnimatorController = attackCombo[comboCounter].animatorOV;
                animator.Play("Attack", 0, 0); // Play attack animation
                comboCounter++; // Increment combo counter
                lastClickedTime = Time.time; // Update last clicked time

                // Reset combo counter if the end of the combo is reached
                if (comboCounter >= attackCombo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    private IEnumerator AbilitySequence()
    {
        // Wait for a short duration before activating the ability
        yield return new WaitForSeconds(0.25f);
        lightning.gameObject.SetActive(true); // Activate lightning effect
        lightning.Play(); // Play lightning visual effect
    }

    void ExitAttack()
    {
        // Check if the attack animation is almost finished
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1); // Schedule EndCombo method to be called after a delay
        }
    }

    void EndCombo()
    {
        comboCounter = 0; // Reset combo counter
        lastComboEnd = Time.time; // Update the time when the combo ended
        IsAttacking = false; // Set attacking state to false
        OnAttackEnd?.Invoke(); // Trigger attack end event
    }
}


