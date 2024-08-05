using Item_Scripts;
using UnityEngine;

public class WeaponSlotHolder : MonoBehaviour
{
    // Reference to a transform that can override the default parent transform
    public Transform parentOverride;

    // Boolean to check if this slot is for the left hand
    public bool isLeftHandSlot;

    // Boolean to check if this slot is for the right hand
    public bool isRightHandSlot;

    // The currently loaded weapon model in this slot
    public GameObject currentWeaponModel;

    // Method to deactivate the current weapon model without destroying it
    public void UnloadWeapon()
    {
        if (currentWeaponModel != null)
        {
            // Deactivate the weapon model if it's currently assigned
            currentWeaponModel.SetActive(false);
        }
    }

    // Method to destroy the current weapon model
    public void UnloadWeaponAndDestroy()
    {
        if (currentWeaponModel != null)
        {
            // Destroy the weapon model if it's currently assigned
            Destroy(currentWeaponModel);
        }
    }
    
    // Method to load a new weapon model into this slot
    public void LoadWeaponModel(WeaponItem weaponItem)
    {
        // First, unload and destroy the currently assigned weapon model
        UnloadWeaponAndDestroy();
        
        // If no weapon item is provided, exit the method
        if (weaponItem == null)
        {
            // No weapon item provided, so no action is taken
            return;
        }
        
        // Instantiate the new weapon model from the prefab
        GameObject model = Instantiate(weaponItem.ModelPrefab);
        
        if (model != null)
        {
            // If a parentOverride is set, use it as the parent; otherwise, use this object's transform
            if (parentOverride != null)
            {
                model.transform.parent = parentOverride;
            }
            else
            {
                model.transform.parent = transform;
            }

            // Reset the model's local position, rotation, and scale to default values
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;
        }

        // Update the current weapon model reference to the new model
        currentWeaponModel = model;
    }
}

