using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] private float maxHealth;
    [SerializeField] private int attackDamage;
    [SerializeField] private int defence;
    [SerializeField] private float magicReserve;
    
    
    public float MaxHealth => maxHealth;
    public float MagicReserve => magicReserve;
    public int AttackDamage => attackDamage;
    public int Defence => defence;
    
}
