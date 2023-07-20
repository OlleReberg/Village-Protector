using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage; // The damage value of the weapon
    private BoxCollider damageCollider; // The BoxCollider component attached to the weapon
    [SerializeField] private WeaponstatsSO weaponStats; // Reference to a ScriptableObject that contains weapon stats
    
    private void Awake()
    {
        damageCollider = GetComponent<BoxCollider>(); // Assign the BoxCollider component to the triggerBox variable
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
        Debug.Log(damageCollider + "collider should be enabled");
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider collision)
    {
        var enemy = collision.GetComponent<Enemy>(); // Get the Enemy component from the collided object, if present
        var weaponDamage = weaponStats.Damage; // Calculate the total damage of the weapon
        if (enemy != null)
        {
            enemy.currentHealth -= damage; // Reduce the enemy's current health by the weapon's damage value
            enemy.animator.SetTrigger("damage"); // Trigger the "damage" animation on the enemy's animator component
            Debug.Log(collision);
            Debug.Log("Dealing " + weaponDamage + " damage to the enemy"); // Log the damage dealt to the enemy
        }
    }

    public void EnableTriggerBox()
    {
        damageCollider.enabled = true; // Enable the triggerBox (BoxCollider)
    }

    public void DisableTriggerBox()
    {
        damageCollider.enabled = false; // Disable the triggerBox (BoxCollider)
    }
}
