using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Item/Weapon Stats")]
public class WeaponstatsSO : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private int fire;
    [SerializeField] private int lightning;
    [SerializeField] private int dark;
    
    public int Damage => damage;
    public int Fire => fire;
    public int Dark => dark;
    public int Lightning => lightning;
}
