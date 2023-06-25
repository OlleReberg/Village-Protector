using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage; // The damage value of the weapon
    private BoxCollider triggerBox; // The BoxCollider component attached to the weapon
    [SerializeField] private WeaponstatsSO weaponStats; // Reference to a ScriptableObject that contains weapon stats
    
    private void Start()
    {
        triggerBox = GetComponent<BoxCollider>(); // Assign the BoxCollider component to the triggerBox variable
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>(); // Get the Enemy component from the collided object, if present
        var weaponDamage = weaponStats.Damage + weaponStats.Dark + weaponStats.Fire + weaponStats.Lightning; // Calculate the total damage of the weapon //TODO: fix this to be more modular
        if (enemy != null)
        {
            enemy.currentHealth -= damage; // Reduce the enemy's current health by the weapon's damage value
            enemy.animator.SetTrigger("damage"); // Trigger the "damage" animation on the enemy's animator component
            Debug.Log(other);
            Debug.Log("Dealing " + weaponDamage + " damage to the enemy"); // Log the damage dealt to the enemy
        }
    }

    public void EnableTriggerBox()
    {
        triggerBox.enabled = true; // Enable the triggerBox (BoxCollider)
    }

    public void DisableTriggerBox()
    {
        triggerBox.enabled = false; // Disable the triggerBox (BoxCollider)
    }
}
