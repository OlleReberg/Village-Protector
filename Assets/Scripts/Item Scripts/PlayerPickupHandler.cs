using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PlayerPickupHandler : MonoBehaviour
{
    public PlayerInventory playerInventory; // Reference to the player's inventory script

    private void Start()
    {
        // Find the PlayerInventory script and assign it to playerInventory
        playerInventory = FindObjectOfType<PlayerInventory>();

        // Check if playerInventory is found, otherwise output a warning message
        if (playerInventory == null)
        {
            Debug.LogWarning("PlayerInventory script not found.");
        }
    }

    private void OnEnable()
    {
        // Subscribe to the onPickup event of the PickupItem script
        FindObjectOfType<Loot>()?.onPickup.AddListener(Pickup);
    }

    private void OnDisable()
    {
        // Unsubscribe from the onPickup event of the PickupItem script
        FindObjectOfType<Loot>()?.onPickup.RemoveListener(Pickup);
    }

    private void Pickup()
    {
        // Perform any actions related to picking up the item (e.g., add it to the player's inventory)
        if (playerInventory != null)
        {
            playerInventory.PickupLoot(FindObjectOfType<Loot>().item);
        }
    }
}

