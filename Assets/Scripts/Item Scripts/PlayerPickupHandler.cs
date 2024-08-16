using UnityEngine;
using PlayerScripts;  // Assuming PlayerInventory is under this namespace
using UI_Scripts;
using UnityEngine.UIElements; // Assuming PlayerInventoryUI is under this namespace

public class PlayerPickupHandler : MonoBehaviour
{
    public PlayerInventory playerInventory; // Reference to the player's inventory script for backend operations
    public UIDocument uiDocument; // Assign in Inspector
    private PlayerInventoryUI playerInventoryUI;

    private void Start()
    {
        // Find the PlayerInventory script and assign it to playerInventory
        playerInventory = FindObjectOfType<PlayerInventory>();

        // Attempt to get the PlayerInventoryUI script from the UIDocument
        playerInventoryUI = uiDocument.GetComponent<PlayerInventoryUI>();

        if (playerInventoryUI == null)
        {
            Debug.LogError("PlayerPickupHandler: PlayerInventoryUI script not found.");
        }

        // Check if playerInventory and playerInventoryUI are found, otherwise output warning messages
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
            playerInventory.AddItemToInventory(FindObjectOfType<Loot>().item);

            // Update the inventory UI after picking up the item
            if (playerInventoryUI != null)
            {
                playerInventoryUI.UpdateUI();
            }
        }
    }
}



