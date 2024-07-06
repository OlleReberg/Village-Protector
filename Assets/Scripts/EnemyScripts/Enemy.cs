using PlayerScripts;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // The stats of this enemy
    [SerializeField] private EnemyStatsSO enemyStats;
    public Animator animator;
    private PlayerStats playerStats;
    
    private EnemyMovement enemyMovement; //get enemymovement script
    public float currentHealth; // The current health of the enemy
    private float attackCooldown = 0f; // The time until the enemy can attack again
    private float abilityCooldown = 0f; // The time until the enemy can use their unique ability again
    private GameObject player; // The player object that the enemy will be attacking
    private bool isAttacking = false; // Whether or not the enemy is currently attacking the player

    private void Awake()
    {
        // Set the current health of the enemy to their maximum health
        currentHealth = enemyStats.MaxHealth;
        
        //Set cooldowns to enemystatSO
        attackCooldown = enemyStats.AttackSpeed;
        abilityCooldown = enemyStats.AbilityCooldown;
    }

    private void Update()
    {
        EnemyAttack();

        EnemyDeath();
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
       currentHealth = Mathf.Clamp(currentHealth, 0, enemyStats.MaxHealth);
        // If the current health of the enemy is less than or equal to 0, destroy the enemy object
        if (currentHealth <= 0)
        {
            animator.ResetTrigger("damage");
            animator.SetTrigger("death");
            
        }
    }

    private void DestroyEnemy()
    {
        //Disabling enemy collider to prevent further hits upon death, then destroying the enemy gameobject
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject);
    }


    private void MoveTowardsPlayer()
    {
        //move towards player transform
        enemyMovement.MoveToTarget(player.transform.position);
    }

    public void SetPlayer(GameObject player)
    {
        // Set the player that the enemy will be attacking
        this.player = player;
    }
}
