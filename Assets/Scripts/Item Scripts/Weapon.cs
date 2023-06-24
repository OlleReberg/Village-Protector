using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    private BoxCollider triggerBox;
    [SerializeField] private WeaponstatsSO weaponStats;
    private void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
    }

   
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        var weaponDamage = weaponStats.Damage + weaponStats.Dark + weaponStats.Fire + weaponStats.Lightning;
        if (enemy != null)
        {
            enemy.currentHealth -= damage;
            enemy.animator.SetTrigger("damage");
            Debug.Log("Dealing " + weaponDamage + " damage to the enemy");
        }
    }

    public void EnableTriggerBox()
    {
        triggerBox.enabled = true;
    }

    public void DisableTriggerBox()
    {
        triggerBox.enabled = false;
    }
}
