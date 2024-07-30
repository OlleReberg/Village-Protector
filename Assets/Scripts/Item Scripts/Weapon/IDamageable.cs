
// Interface to define damageable entities
public interface IDamageable
{
    // Method to apply damage to the entity
    void TakeDamage(float damageAmount);
    
    // Method to get the entity's stats
    // Outputs various stat parameters for the entity
    void GetOwnerStats(out float baseDamage, out float maxHealth,
        out float physRes, out float fireRes, out float darkRes, out float lightningRes);
}

