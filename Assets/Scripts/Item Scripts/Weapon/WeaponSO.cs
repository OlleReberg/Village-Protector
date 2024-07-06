using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float weaponRange;
    
    public int Damage => damage;
    public float WeaponRange => weaponRange;
    
}
