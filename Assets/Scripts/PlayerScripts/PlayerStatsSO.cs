using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int attackDamage;
    
    public int MaxHealth => maxHealth;
    public int AttackDamage => attackDamage;
}
