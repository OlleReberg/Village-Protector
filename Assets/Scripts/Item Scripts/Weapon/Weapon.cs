using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private BoxCollider damageCollider; // The BoxCollider component attached to the weapon
    [SerializeField] private WeaponstatsSO weaponStats; // Reference to a ScriptableObject that contains weapon stats
    public ParticleSystem trail; // Attached particle system to the weapon
    private List<Collider> hitObject = new List<Collider>(); // List to track hit objects during an attack
    [SerializeField] private bool enableCollider; // Flag to enable/disable the weapon collider
    [SerializeField] private float attackSpeed; // Speed of weapon attacks

    private IDamageable owningCharacterDamageable; // Reference to IDamageable component on the owning character
    private GameObject owningCharacter; // Reference to the owning character's GameObject

    private void Awake()
    {
        // Assign the BoxCollider component to the damageCollider variable
        damageCollider = GetComponent<BoxCollider>();
        damageCollider.gameObject.SetActive(true); // Ensure the collider object is active
        damageCollider.isTrigger = true; // Set the collider to be a trigger to detect collisions without affecting physics
        damageCollider.enabled = enableCollider; // Enable/Disable the collider based on enableCollider flag
    }

    public void EquipWeapon(GameObject owner)
    {
        // Equip the weapon to a character and set the owningCharacterDamageable reference
        owningCharacter = owner;
        owningCharacterDamageable = owner.GetComponent<IDamageable>();
        if (owningCharacterDamageable == null)
        {
            Debug.LogError("The owner does not have an IDamageable component.");
        }
    }

    public void EnableDamageCollider()
    {
        // Enable the damage collider to detect collisions with enemies
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        // Disable the damage collider to prevent further collisions with enemies
        damageCollider.enabled = false;
        hitObject.Clear(); // Clear the list of hit objects
    }

    public float CalculateFinalDamage(IDamageable target)
    {
        // Ensure target is not null
        if (target == null)
        {
            Debug.LogError("Target is null in CalculateFinalDamage.");
            return 0;
        }

        // Get the attacker stats
        if (owningCharacterDamageable != null)
        {
            owningCharacterDamageable.GetOwnerStats(out float baseDamage, out float maxHealth,
                out float physRes, out float fireRes, out float darkRes, out float lightningRes);

            // Calculate weapon damage
            float physicalDamage = weaponStats.Physical + baseDamage;
            float fireDamage = weaponStats.Fire;
            float darkDamage = weaponStats.Dark;
            float lightningDamage = weaponStats.Lightning;

            // Get target's resistances
            target.GetOwnerStats(out float targetBaseDamage, out float targetMaxHealth,
                out float targetPhysRes, out float targetFireRes, out float targetDarkRes, out float targetLightningRes);

            // Calculate damage after resistance
            float finalPhysicalDamage = physicalDamage * (1 - targetPhysRes / (targetPhysRes + 100));
            float finalFireDamage = fireDamage * (1 - targetFireRes / (targetFireRes + 100));
            float finalDarkDamage = darkDamage * (1 - targetDarkRes / (targetDarkRes + 100));
            float finalLightningDamage = lightningDamage * (1 - targetLightningRes / (targetLightningRes + 100));

            // Sum up the total damage
            float totalDamage = finalPhysicalDamage + finalFireDamage + finalDarkDamage + finalLightningDamage;
            return totalDamage;
        }

        return 0; // Return 0 if owning character does not implement IDamageable
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Handle collision with other objects
        if (hitObject.Contains(collision) || collision.gameObject == owningCharacter)
        {
            return; // Ignore if the object is already hit or if it is the owning character
        }

        hitObject.Add(collision); // Add the collided object to the hit list
        if (enableCollider)
        {
            StartCoroutine(AttackDelay()); // Delay the next attack
        }

        var target = collision.GetComponent<IDamageable>(); // Get the IDamageable component from the collided object, if present
        if (target != null)
        {
            // Calculate the damage based on weapon stats and target resistances
            float weaponDamage = CalculateFinalDamage(target);
            target.TakeDamage(weaponDamage); // Deal damage to the target
            Debug.Log("Dealing " + weaponDamage + " damage to the target"); // Log the damage dealt
        }
    }

    IEnumerator AttackDelay()
    {
        // Delay between attacks
        yield return new WaitForSeconds(attackSpeed);
        hitObject.Clear(); // Clear the list of hit objects after the delay
    }
}













