using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy/Enemy Stats")]
public class EnemyStatsSO : ScriptableObject
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float physRes;
    [SerializeField] private float fireRes;
    [SerializeField] private float darkRes;
    [SerializeField] private float lightningRes;
    public float DarkRes => darkRes;
    public float LightningRes => lightningRes;

    [SerializeField] private float abilityCooldown;
    [SerializeField] private string standardAttackName;
    [SerializeField] private string uniqueAbilityName;
    [SerializeField] private float uniqueAbilityDamage;
    
    [SerializeField] private AnimatorOverrideController animatorOV;
    //[SerializeField] private GameObject uniqueAbilityPrefab;

    public float MaxHealth => maxHealth;
    public float AttackRange => attackRange;
    public float AttackSpeed => attackSpeed;
    public float AttackDamage => attackDamage;
    public float PhysRes => physRes;
    public float FireRes => fireRes;
    public float AbilityCooldown => abilityCooldown;
   // public GameObject StandardAttackPrefab => standardAttackPrefab;
   // public GameObject UniqueAbilityPrefab => uniqueAbilityPrefab;
    

    public string StandardAttackName => standardAttackName;
    public string UniqueAbilityName => uniqueAbilityName;
    public float UniqueAbilityDamage => uniqueAbilityDamage;
}
