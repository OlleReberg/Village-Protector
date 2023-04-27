using UnityEngine;

public class Enemy : MonoBehaviour
{
    // The stats of this enemy
    private EnemyStats enemyStats;

    // The current health of the enemy
    private int currentHealth;

    // The time until the enemy can attack again
    private float attackCooldown = 0;

    // The time until the enemy can use their unique ability again
    private float abilityCooldown = 0;

    // The player object that the enemy will be attacking
    private GameObject player;

    // Whether or not the enemy is currently attacking the player
    private bool isAttacking = false;

    private void Awake()
    {
        // Get the EnemyStats component attached to this game object
        enemyStats = GetComponent<EnemyStats>();

        // Set the current health of the enemy to their maximum health
        currentHealth = enemyStats.MaxHealth;
    }

    private void Update()
    {
        // If the player is not null, check if they are within range
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= enemyStats.AttackRange)
            {
                // If the player is within attack range, attack the player
                Attack();
            }
            else if (distanceToPlayer <= enemyStats.VisionRange)
            {
                // If the player is within vision range, move towards the player
                MoveTowardsPlayer();
            }
        }

        // Reduce the time until the next attack can be made
        attackCooldown = Mathf.Max(0, attackCooldown - Time.deltaTime);

        // Reduce the time until the unique ability can be used again
        abilityCooldown = Mathf.Max(0, abilityCooldown - Time.deltaTime);
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

    private void Attack()
    {
        // If the attack cooldown has expired and the enemy is not already attacking, attack the player
        if (attackCooldown <= 0 && !isAttacking)
        {
            // Set the attack cooldown to the attack speed of the enemy
            attackCooldown = 1f / enemyStats.AttackSpeed;

            // Play the attack animation
            // ...

            // Deal damage to the player
            player.GetComponent<PlayerCombatController>().TakeDamage(enemyStats.AttackDamage);
        }
    }

    private void MoveTowardsPlayer()
    {
        // Move towards the player
        // ...
    }

    public void SetPlayer(GameObject player)
    {
        // Set the player that the enemy will be attacking
        this.player = player;
    }
}
