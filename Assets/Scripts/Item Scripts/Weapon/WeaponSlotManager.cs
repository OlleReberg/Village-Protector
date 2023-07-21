using System;
using System.Collections;
using System.Collections.Generic;
using Item_Scripts;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    private WeaponSlotHolder leftHandSlot;
    private WeaponSlotHolder rightHandSlot;

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
        }
        else
        {
            rightHandSlot.LoadWeaponModel(weaponItem);
        }
    }
}
