using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponstatsSO weaponStats;
    private void OnTriggerEnter(Collider other)
    {
        var weaponDamage = weaponStats.Damage + weaponStats.Dark + weaponStats.Fire + weaponStats.Lightning;
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(weaponDamage);
            Debug.Log("Dealing " + weaponDamage + " damage to the enemy");
        }
    }
}
