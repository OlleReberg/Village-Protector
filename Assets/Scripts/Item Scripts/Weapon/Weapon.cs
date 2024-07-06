using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Weapon : MonoBehaviour
{
    private BoxCollider damageCollider; // The BoxCollider component attached to the weapon
    [SerializeField] private WeaponstatsSO weaponStats; // Reference to a ScriptableObject that contains weapon stats
    public ParticleSystem trail; //Attached particle system to weapon
    private List<Collider> hitObject = new List<Collider>();
    [SerializeField] private GameObject owningCharacter;
    [SerializeField] private bool enableCollider;
    [SerializeField] private float attackSpeed;
    private void Awake()
    {
        damageCollider = GetComponent<BoxCollider>(); // Assign the BoxCollider component to the damageCollider variable
        damageCollider.gameObject.SetActive(true); // Ensure the collider object is active
        damageCollider.isTrigger = true; // Set the collider to be a trigger to detect collisions without affecting physics
        damageCollider.enabled = enableCollider; // Disable the collider by default until the attack is initiated
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true; // Enable the damage collider to detect collisions with enemies
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false; // Disable the damage collider to prevent further collisions with enemies
        hitObject.Clear();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.name);
        if (hitObject.Contains(collision) || collision.gameObject == owningCharacter)
        {
            return;
        }
        hitObject.Add(collision);
        if (enableCollider)
        {
            StartCoroutine(AttackDelay());
        }
        
        var enemy = collision.GetComponent<IDamageable>(); // Get the Enemy component from the collided object, if present
        var weaponDamage = weaponStats.Damage; // Calculate the total damage of the weapon
        
        if (enemy != null)
        {
            enemy.TakeDamage(weaponDamage); // Reduce the enemy's current health by the weapon's damage value
            Debug.Log("Dealing " + weaponDamage + " damage to the enemy"); // Log the damage dealt to the enemy
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackSpeed);
        hitObject.Clear();
    }

    public void EquipWeapon(GameObject owner)
    {
        owningCharacter = owner;
    }
}








