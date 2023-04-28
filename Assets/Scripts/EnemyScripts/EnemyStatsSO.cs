using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy/Enemy Stats")]
public class EnemyStatsSO : ScriptableObject
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float visionRange;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackSpeed;

    [SerializeField] private float abilityCooldown;
    [SerializeField] private string standardAttackName;
    [SerializeField] private string uniqueAbilityName;
    [SerializeField] private float uniqueAbilityDamage;
    
   // [SerializeField] private GameObject standardAttackPrefab;
    //[SerializeField] private GameObject uniqueAbilityPrefab;

    public int MaxHealth => maxHealth;
    public float MovementSpeed => movementSpeed;
    public float AttackRange => attackRange;
    public float VisionRange => visionRange;
    public int AttackDamage => attackDamage;
    public float AttackSpeed => attackSpeed;
    public float AbilityCooldown => abilityCooldown;
   // public GameObject StandardAttackPrefab => standardAttackPrefab;
   // public GameObject UniqueAbilityPrefab => uniqueAbilityPrefab;
    

    public string StandardAttackName => standardAttackName;
    public string UniqueAbilityName => uniqueAbilityName;
    public float UniqueAbilityDamage => uniqueAbilityDamage;
}
