using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStats;
    public GameObject weapon;

    private int currentHealth;

    private Animator animator;
    private float nextFireTime = 0f; // Time of the next available attack
    [SerializeField] private static int noOfClicks = 0; // Number of clicks in a combo
    private float lastClickedTime = 0; // Time of the last click
    private float maxComboDelay = 1; // Maximum delay between clicks to maintain combo

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the same GameObject
    }

    private void Update()
    {
        // Check if the current attack animation has reached a certain point and reset the corresponding trigger
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("hit 1"))
        {
            animator.SetBool("hit 1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("hit 2"))
        {
            animator.SetBool("hit 2", false);
        }

        // Reset the combo if the time between clicks exceeds the maximum combo delay
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        // Check if it's time for the next attack and the player has clicked the mouse button
        if (Time.time > nextFireTime && Input.GetMouseButtonDown(0))
        {
            OnClick(); // Perform the attack
        }
    }

    void OnClick()
    {
        lastClickedTime = Time.time; // Record the time of the click
        noOfClicks++; // Increase the number of clicks in the combo

        if (noOfClicks == 1)
        {
            animator.SetBool("hit 1", true); // Trigger the first hit animation
        }

        Mathf.Clamp(noOfClicks, 0, 3); // Limit the number of clicks to a maximum of 3

        // Trigger the second hit animation if the combo conditions are met
        if (noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7
                            && animator.GetCurrentAnimatorStateInfo(0).IsName("hit 1"))
        {
            animator.SetBool("hit 1", false);
            animator.SetBool("hit 2", true);
        }
    }

    public void TakeDamage(int amount)
    {
        // Reduce the current health of the enemy by the amount of damage taken
        currentHealth -= amount;

        // If the current health of the enemy is less than or equal to 0, destroy the enemy object
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DealDamage(GameObject target, int damage)
    {
        if (target == CompareTag("Enemy")) // Check if the target object has the tag "Enemy"
        {
            damage = playerStats.AttackDamage;
            Debug.Log("Player hit " + target + "for " + damage);
            //Enemy enemy;
            // if (enemy.currentHealth != null)
            // {
            //     // Call the TakeDamage function on the target's health controller
            //     enemy.TakeDamage(damage);
            // }
        }
    }
}
