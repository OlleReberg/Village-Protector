using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private BoxCollider damageCollider; // The BoxCollider component attached to the weapon
    [SerializeField] private WeaponstatsSO weaponStats; // Reference to a ScriptableObject that contains weapon stats
    
    private void Awake()
    {
        damageCollider = GetComponent<BoxCollider>(); // Assign the BoxCollider component to the damageCollider variable
        damageCollider.gameObject.SetActive(true); // Ensure the collider object is active
        damageCollider.isTrigger = true; // Set the collider to be a trigger to detect collisions without affecting physics
        damageCollider.enabled = false; // Disable the collider by default until the attack is initiated
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true; // Enable the damage collider to detect collisions with enemies
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false; // Disable the damage collider to prevent further collisions with enemies
    }

    private void OnTriggerEnter(Collider collision)
    {
        var enemy = collision.GetComponent<Enemy>(); // Get the Enemy component from the collided object, if present
        var weaponDamage = weaponStats.Damage; // Calculate the total damage of the weapon
        
        if (enemy != null)
        {
            enemy.currentHealth -= weaponDamage; // Reduce the enemy's current health by the weapon's damage value
            enemy.animator.SetTrigger("damage"); // Trigger the "damage" animation on the enemy's animator component
            Debug.Log(collision);
            Debug.Log("Dealing " + weaponDamage + " damage to the enemy"); // Log the damage dealt to the enemy
        }
    }
}








