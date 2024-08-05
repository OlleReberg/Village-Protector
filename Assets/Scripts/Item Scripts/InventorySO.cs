namespace Item_Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        [SerializeField] private List<InventoryItem> inventoryItems = new List<InventoryItem>();
        [field: SerializeField] public int Size { get; private set; } = 10;

        // Initializes the inventory with empty items
        public void Initialize()
        {
            if (Size < 0)
            {
                Debug.LogError("Inventory size cannot be negative.");
                return;
            }

            inventoryItems = Enumerable.Repeat(InventoryItem.GetEmptyItem(), Size).ToList();
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        // Adds an item to the inventory, stacking if possible
        public void AddItem(ItemSO item, int quantity)
        {
            bool itemAdded = false;

            // Try to stack item first
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (!inventoryItems[i].isEmpty && inventoryItems[i].itemSO == item)
                {
                    inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].iQuantity + quantity);
                    itemAdded = true;
                    break;
                }
            }

            // If not stacked, find an empty slot
            if (!itemAdded)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (inventoryItems[i].isEmpty)
                    {
                        inventoryItems[i] = new InventoryItem { itemSO = item, iQuantity = quantity };
                        itemAdded = true;
                        break;
                    }
                }
            }

            // If no slot available, log a warning
            if (!itemAdded)
            {
                Debug.LogWarning("Inventory is full. Item could not be added.");
            }

            // Invoke event once after attempting to add the item
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        // Returns a dictionary of non-empty inventory items
        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            var currentInventory = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (!inventoryItems[i].isEmpty)
                {
                    currentInventory[i] = inventoryItems[i];
                }
            }
            return currentInventory;
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int iQuantity;
        public ItemSO itemSO;

        // Determines if the inventory slot is empty
        public bool isEmpty => itemSO == null;

        // Changes the quantity of the item
        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                itemSO = itemSO,
                iQuantity = newQuantity,
            };
        }

        // Returns an empty inventory item
        public static InventoryItem GetEmptyItem()
            => new InventoryItem
            {
                itemSO = null,
                iQuantity = 0,
            };
    }
}

