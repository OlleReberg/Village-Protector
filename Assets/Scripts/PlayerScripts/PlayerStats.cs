using UnityEngine.Serialization;

namespace PlayerScripts
{
    using UnityEngine;

    // Class representing the player's stats
    public class PlayerStats : MonoBehaviour, IDamageable
    {
        public PlayerStatsSO playerstatsSO; // Reference to a ScriptableObject that contains player stats
        public float currentHealth;
        public float maxHealth; // Current health of the player
        public float currentMana; // Current mana of the player
        public float attackDamage; // Current attack damage of the player
        public float defence; // Current defence of the player

        private int combatRating = 1; // Player's combat rating, a hidden value that increases with battle victories

        public void Awake()
        {
            // Initialize player stats based on the values from the ScriptableObject
            currentHealth = playerstatsSO.MaxHealth;
            maxHealth = playerstatsSO.MaxHealth;
            currentMana = playerstatsSO.MagicReserve;
            attackDamage = playerstatsSO.AttackDamage;
            defence = playerstatsSO.PhysRes;
            combatRating = playerstatsSO.CombatRating;
        }

        public void ApplyStatsGains(float healthGain, float manaGain, float attackGain, float defenceGain, int combatRatingGain)
        {
            // Apply gains to the player's stats
            maxHealth += healthGain;
            currentHealth = Mathf.Clamp(currentHealth + healthGain, 0, maxHealth);
            currentMana += manaGain;
            attackDamage += attackGain;
            defence += defenceGain;
            combatRating += combatRatingGain; // Increase the player's combat rating
        }

        public void OnBattleVictory(PlayerStatsUI ui)
        {
            // Calculate stat gains upon battle victory
            float healthGain = Random.Range(5, 12); // Example health gain
            float manaGain = Random.Range(5, 12); // Example mana gain
            float attackGain = Random.Range(1, 3); // Example attack gain
            float defenceGain = Random.Range(1, 3); // Example defence gain
            int combatGain = 1;
            // Show the stat gains on the UI
            ui.ShowStatsGains(healthGain, manaGain, attackGain, defenceGain);
        }

        public void TakeDamage(float damageAmount)
        {
            // Apply damage to the player
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                // Handle player death (implementation not shown)
                Debug.Log("Player has died.");
            }
        }

        public void GetOwnerStats(out float baseDamage, out float maxHealth,
            out float physRes, out float fireRes, out float darkRes, out float lightningRes)
        {
            baseDamage = attackDamage;
            maxHealth = playerstatsSO.MaxHealth;
            physRes = playerstatsSO.PhysRes;
            fireRes = playerstatsSO.FireRes;
            darkRes = playerstatsSO.DarkRes;
            lightningRes = playerstatsSO.LightningRes;
        }
    }
}
