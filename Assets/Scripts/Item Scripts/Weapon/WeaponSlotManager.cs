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
        WeaponSlotHolder[] weaponSlotHolders = GetComponentsInChildren<WeaponSlotHolder>();
        foreach (WeaponSlotHolder weaponSlot  in weaponSlotHolders)
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
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeaponCollider();
        }
        else
        {
            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightWeaponCollider();
        }
    }

    #region Handle Weapon's Damage Collider
    
    public void LoadLeftWeaponCollider()
    {
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<Weapon>();
    }

    public void LoadRightWeaponCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<Weapon>();
    }

    public void OpenRightWeaponCollider()
    {
        rightHandDamageCollider.EnableDamageCollider();
    }

    public void OpenLeftWeaponCollider()
    {
        leftHandDamageCollider.EnableDamageCollider();
    }

    public void CloseRightWeaponCollider()
    {
        rightHandDamageCollider.DisableDamageCollider();
    }

    public void CloseLeftWeaponCollider()
    {
        leftHandDamageCollider.DisableDamageCollider();
    }
    
    #endregion
}
