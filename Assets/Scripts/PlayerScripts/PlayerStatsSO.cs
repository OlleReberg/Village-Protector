using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float magicReserve;
    [SerializeField] private float attackDamage;
    [SerializeField] private float physRes;
    [SerializeField] private float fireRes;
    [SerializeField] private float darkRes;
    [SerializeField] private float lightningRes;
    
    [SerializeField] private int combatRating;
    
    
    
    public float MaxHealth => maxHealth;
    public float MagicReserve => magicReserve;
    public float AttackDamage => attackDamage;
    public float PhysRes => physRes;
    public float FireRes => fireRes;
    public float DarkRes => darkRes;
    public float LightningRes => lightningRes;
    public int CombatRating => combatRating;

}
