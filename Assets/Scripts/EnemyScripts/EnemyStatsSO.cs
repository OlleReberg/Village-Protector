using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy/Enemy Stats")]
public class EnemyStatsSO : ScriptableObject
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int attackDamage;
    [SerializeField] private int armor;

    [SerializeField] private float abilityCooldown;
    [SerializeField] private string standardAttackName;
    [SerializeField] private string uniqueAbilityName;
    [SerializeField] private float uniqueAbilityDamage;
    
   // [SerializeField] private GameObject standardAttackPrefab;
    //[SerializeField] private GameObject uniqueAbilityPrefab;

    public int MaxHealth => maxHealth;
    public float AttackRange => attackRange;
    public float AttackSpeed => attackSpeed;
    public int AttackDamage => attackDamage;
    public int Armor => armor;
    public float AbilityCooldown => abilityCooldown;
   // public GameObject StandardAttackPrefab => standardAttackPrefab;
   // public GameObject UniqueAbilityPrefab => uniqueAbilityPrefab;
    

    public string StandardAttackName => standardAttackName;
    public string UniqueAbilityName => uniqueAbilityName;
    public float UniqueAbilityDamage => uniqueAbilityDamage;
}
