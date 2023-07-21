using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerCombatController : MonoBehaviour
{
    private Animator animator; // Reference to the Animator component attached to the player
    private Weapon weapon; // Reference to the Weapon component attached to the player
    public List<AttackSO> attckCombo; // List of AttackSO (ScriptableObject) containing attack combos
    private float lastClickedTime; // Time of the last click
    private float lastComboEnd; // Time when the last combo ended
    private int comboCounter; // Counter for tracking the current combo index
    public float delay = 1f;
    public VisualEffect lightning;

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Assign the Animator component to the animator variable
        lightning.gameObject.SetActive(false);
    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            Attack(); // Perform an attack when the "Fire1" button is pressed
        }
        ExitAttack(); // Check if the attack animation has finished and exit the attack state
        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(AbilitySequence());
        }
    }
    void Attack()
    {
        if (Time.time - lastComboEnd > 0.5f && comboCounter < attckCombo.Count)
        {
            CancelInvoke("EndCombo"); // Cancel the previous Invoke to end the combo

            if (Time.time - lastClickedTime >= 0.3f)
            {
                // Set the animator's runtime controller to the current combo's animator override
                animator.runtimeAnimatorController = attckCombo[comboCounter].animatorOV;
                animator.Play("Attack", 0, 0); // Play the "Attack" animation from the beginning
                //weapon.damage = attckCombo[comboCounter].damage; // Set the weapon's damage to the damage value of the current combo
                comboCounter++; // Increment the combo counter
                lastClickedTime = Time.time; // Update the last clicked time

                if (comboCounter >= attckCombo.Count)
                {
                    comboCounter = 0; // Reset the combo counter if it exceeds the number of available combos
                }
            }
        }
    }

    private IEnumerator AbilitySequence()
    {
        yield return new WaitForSeconds(0.25f);
        lightning.gameObject.SetActive(true);
        lightning.Play();
    }

    void ExitAttack()
    {
        // Invoke the EndCombo method after 1 second if the attack animation is almost complete and tagged as "Attack"
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f 
            && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
        }
    }

    void EndCombo()
    {
        comboCounter = 0; // Reset the combo counter to start a new combo
        lastComboEnd = Time.time; // Update the last combo end time
    }
}
