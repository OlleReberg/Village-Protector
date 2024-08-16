using Item_Scripts;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    public Transform rightHandSlot;
    public Transform leftHandSlot;
    public WeaponItem testWeaponItem; // For testing purposes

    private Weapon currentRightHandWeapon;
    private Weapon currentLeftHandWeapon;

    private void Start()
    {
        // Check if this is a testing scenario
        if (testWeaponItem != null)
        {
            EquipWeapon(testWeaponItem, true); // Equip in the right hand for testing
        }
    }

    //Model instantiation handled via WeaponSlotHolder which is attached to player model hand
    public void EquipWeapon(WeaponItem weaponItem, bool isRightHand)
    {
        if (weaponItem == null)
        {
            Debug.LogError("WeaponSlotManager: No weapon item provided to equip.");
            return;
        }

        Transform slotTransform = isRightHand ? rightHandSlot : leftHandSlot;
        WeaponSlotHolder slotHolder = slotTransform.GetComponent<WeaponSlotHolder>();

        if (slotHolder != null)
        {
            slotHolder.LoadWeaponModel(weaponItem);
        }

        // Additional logic here for setting up the weapon after it's visually equipped
    }

    public void UnequipWeapon(bool isRightHand)
    {
        Transform slotTransform = isRightHand ? rightHandSlot : leftHandSlot;
        WeaponSlotHolder slotHolder = slotTransform.GetComponent<WeaponSlotHolder>();

        if (slotHolder != null)
        {
            slotHolder.UnloadWeaponAndDestroy();
        }

        // Additional cleanup logic here
    }

    private void ClearSlot(Transform slotTransform)
    {
        foreach (Transform child in slotTransform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SwapWeapons(WeaponItem newWeaponItem, bool isRightHand)
    {
        UnequipWeapon(isRightHand);
        EquipWeapon(newWeaponItem, isRightHand);
    }

    private void InitializeWeapon(Weapon weapon)
    {
        if (weapon == null) return;

        // Ensure the collider is disabled at start
        weapon.DisableDamageCollider();

        if (weapon.trail != null)
        {
            weapon.trail.Stop();
        }
    }

    public void EnableWeaponTrail(bool isRightHand)
    {
        Weapon weapon = isRightHand ? currentRightHandWeapon : currentLeftHandWeapon;
        if (weapon != null && weapon.trail != null)
        {
            weapon.trail.gameObject.SetActive(true);
        }
    }

    public void DisableWeaponTrail(bool isRightHand)
    {
        Weapon weapon = isRightHand ? currentRightHandWeapon : currentLeftHandWeapon;
        if (weapon != null && weapon.trail != null)
        {
            weapon.trail.gameObject.SetActive(false);
        }
    }

    public void EnableWeaponCollider(bool isRightHand)
    {
        Weapon weapon = isRightHand ? currentRightHandWeapon : currentLeftHandWeapon;
        if (weapon != null)
        {
            weapon.EnableDamageCollider();
        }
    }

    public void DisableWeaponCollider(bool isRightHand)
    {
        Weapon weapon = isRightHand ? currentRightHandWeapon : currentLeftHandWeapon;
        if (weapon != null)
        {
            weapon.DisableDamageCollider();
        }
    }
}




