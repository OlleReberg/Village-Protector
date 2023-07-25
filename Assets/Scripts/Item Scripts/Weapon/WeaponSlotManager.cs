using System;
using System.Collections;
using System.Collections.Generic;
using Item_Scripts;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    private WeaponSlotHolder leftHandSlot;
    private WeaponSlotHolder rightHandSlot;

    private Weapon leftHandDamageCollider;
    private Weapon rightHandDamageCollider;

    private void Awake()
    {
        // Get all WeaponSlotHolder components attached to this object or its children
        WeaponSlotHolder[] weaponSlotHolders = GetComponentsInChildren<WeaponSlotHolder>();

        // Loop through each WeaponSlotHolder to find left and right hand slots
        foreach (WeaponSlotHolder weaponSlot in weaponSlotHolders)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (isLeft)
        {
            leftHandSlot.LoadWeaponModel(weaponItem); // Load the weapon model into the left hand slot
            LoadLeftWeaponCollider(); // Load the left hand weapon's damage collider reference
        }
        else
        {
            rightHandSlot.LoadWeaponModel(weaponItem); // Load the weapon model into the right hand slot
            LoadRightWeaponCollider(); // Load the right hand weapon's damage collider reference
        }
    }

    #region Handle Weapon's Damage Collider
    
    public void LoadLeftWeaponCollider()
    {
        // Get the Weapon script component from the left hand weapon model's children
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<Weapon>();
    }

    public void LoadRightWeaponCollider()
    {
        // Get the Weapon script component from the right hand weapon model's children
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<Weapon>();
    }

    public void OpenRightWeaponCollider()
    {
        // Enable the damage collider of the right hand weapon
        rightHandDamageCollider.EnableDamageCollider();
    }

    public void OpenLeftWeaponCollider()
    {
        // Enable the damage collider of the left hand weapon
        leftHandDamageCollider.EnableDamageCollider();
    }

    public void CloseRightWeaponCollider()
    {
        // Disable the damage collider of the right hand weapon
        rightHandDamageCollider.DisableDamageCollider();
    }

    public void CloseLeftWeaponCollider()
    {
        // Disable the damage collider of the left hand weapon
        leftHandDamageCollider.DisableDamageCollider();
    }
    
    #endregion
}
