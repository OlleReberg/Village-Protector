using UnityEngine;

public class Enemy : MonoBehaviour
{
    // The stats of this enemy
    [SerializeField] private EnemyStatsSO enemyStats;
    public Animator animator;
    
    //get enemymovement script
    private EnemyMovement enemyMovement;

    // The current health of the enemy
    public int currentHealth;

    // The time until the enemy can attack again
    private float attackCooldown = 0f;

    // The time until the enemy can use their unique ability again
    private float abilityCooldown = 0f;

    // The player object that the enemy will be attacking
    private GameObject player;

    // Whether or not the enemy is currently attacking the player
    private bool isAttacking = false;

    private void Awake()
    {
        // Set the current health of the enemy to their maximum health
        currentHealth = enemyStats.MaxHealth;
        
        //Set cooldowns to enemystatSO
        //attackCooldown = enemyStats.AttackSpeed;
      //  abilityCooldown = enemyStats.AbilityCooldown;
    }

    private void Update()
    {
        // Reduce the time until the next attack can be made
        //attackCooldown = Mathf.Max(0, attackCooldown - Time.deltaTime);

        // Reduce the time until the unique ability can be used again
        //abilityCooldown = Mathf.Max(0, abilityCooldown - Time.deltaTime);
    }

    public void TakeDamage(int amount)
    {
        // Reduce the current health of the enemy by the amount of damage taken
        currentHealth -= amount;

        // If the current health of the enemy is less than or equal to 0, destroy the enemy object
        if (currentHealth <= 0)
        {
            //Destroy(gameObject);
            animator.SetTrigger("death");
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("damage");
        }
        
    }

    private void Attack()
    {
        //handled elsewhere at the moment
    }

    private void MoveTowardsPlayer()
    {
        enemyMovement.MoveToTarget(player.transform.position);
    }

    public void SetPlayer(GameObject player)
    {
        // Set the player that the enemy will be attacking
        this.player = player;
    }
}
