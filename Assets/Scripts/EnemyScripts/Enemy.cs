using PlayerScripts;
using UnityEngine;

// Class representing an enemy character
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyStatsSO enemyStats; // The stats of this enemy
    public Animator animator; // Animator component for controlling animations
    private PlayerStats playerStats; // Reference to the player's stats
    
    private EnemyMovement enemyMovement; // Reference to the EnemyMovement script
    public float currentHealth; // The current health of the enemy
    private float attackCooldown = 0f; // The time until the enemy can attack again
    private float abilityCooldown = 0f; // The time until the enemy can use their unique ability again
    private GameObject player; // The player object that the enemy will be attacking
    private bool isAttacking = false; // Whether or not the enemy is currently attacking the player
    private Weapon weapon; // Reference to the weapon component

    private void Awake()
    {
        // Initialize the current health of the enemy to their maximum health
        currentHealth = enemyStats.MaxHealth;
        
        // Set cooldowns based on enemy stats
        attackCooldown = enemyStats.AttackSpeed;
        abilityCooldown = enemyStats.AbilityCooldown;

        weapon = GetComponentInChildren<Weapon>();
        weapon.EquipWeapon(gameObject);
    }

    private void Update()
    {
        // Handle enemy attack logic each frame
        EnemyAttack();
    }

    private void EnemyAttack()
    {
        // Reduce the time until the next attack can be made
        attackCooldown = Mathf.Max(0, attackCooldown - Time.deltaTime);

        // Reduce the time until the unique ability can be used again
        abilityCooldown = Mathf.Max(0, abilityCooldown - Time.deltaTime);
    }

    public void EnemyDeath()
    {
        // Clamp the current health between 0 and maximum health
        currentHealth = Mathf.Clamp(currentHealth, 0, enemyStats.MaxHealth);

        // Trigger the death animation if health is zero or below
        animator.ResetTrigger("damage");
        animator.SetTrigger("death");
    }

    private void DestroyEnemy()
    {
        // Disable the enemy's collider to prevent further hits and destroy the game object
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
    }

    private void MoveTowardsPlayer()
    {
        // Move towards the player's position
        enemyMovement.MoveToTarget(player.transform.position);
    }

    public void SetPlayer(GameObject player)
    {
        // Set the player that the enemy will be attacking
        this.player = player;
    }

    public void TakeDamage(float damageAmount)
    {
        // Reduce current health by the damage amount
        currentHealth -= damageAmount;

        // If health drops to zero or below, handle enemy death
        if (currentHealth <= 0)
        {
            EnemyDeath();
        }
        else
        {
            // Trigger damage animation if still alive
            animator.SetTrigger("damage");
        }
    }

    public void GetOwnerStats(out float baseDamage, out float maxHealth,
        out float physRes, out float fireRes, out float darkRes, out float lightningRes)
    {
        // Get the enemy's stats and output them
        var stats = enemyStats;
        baseDamage = stats.AttackDamage;
        maxHealth = stats.MaxHealth;
        physRes = stats.PhysRes;
        fireRes = stats.FireRes;
        darkRes = stats.DarkRes;
        lightningRes = stats.LightningRes;
    }

    public void OpenWeaponCollider()
    {
        // Enable the weapon's damage collider
        weapon.EnableDamageCollider();
    }
    
    public void CloseWeaponCollider()
    {
        // Disable the weapon's damage collider
        weapon.DisableDamageCollider();
    }
}

