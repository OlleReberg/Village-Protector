using System.Collections;
using System.Collections.Generic;
using Item_Scripts;
using UnityEngine;

using UnityEngine;

public class PlayerPickupHandler : MonoBehaviour
{
    public PlayerInventory playerInventory; // Reference to the player's inventory script
    public InventorySlotUI[] inventorySlots; // Array to store the inventory slot UI components

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

    public void PopulateInventorySlots(InventorySlotUI[] slots)
    {
        // Store the references to the inventory slot UI components
        inventorySlots = slots;
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

            // Update the inventory UI after picking up the item
            UpdateInventoryUI();
        }
    }

    private void UpdateInventoryUI()
    {
        // Loop through the player's inventory and update the UI slots accordingly
        for (int i = 0; i < playerInventory.playerInventory.Count; i++)
        {
            if (i < inventorySlots.Length)
            {
                InventorySlotUI slotUI = inventorySlots[i];
                ItemSO item = playerInventory.playerInventory[i];

                // Set the item icon and quantity in the UI slot using 'item'
                slotUI.itemIconImage.sprite = item.ItemIcon;
                slotUI.quantityText.text = "x" + item.Quantity.ToString();
            }
        }
    }
}

