using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Item_Scripts;


namespace PlayerScripts
{
    public class PlayerInventory : MonoBehaviour
    {
        public static int TotalSlots = 35;
        public static int TotalHorizontal = 6;
        public List<ItemSO> playerInventory = new List<ItemSO>(); // List to store inventory items
        private List<IInventoryObserver> inventoryObservers = new List<IInventoryObserver>(); // Observers for inventory changes

        public void AddItemToInventory(ItemSO item)
        {
            playerInventory.Add(item);
            NotifyObserversItemAdded(item);
        }

        public void RemoveItemFromInventory(ItemSO item)
        {
            playerInventory.Remove(item);
            NotifyObserversItemRemoved(item);
        }

        public void AddObserver(IInventoryObserver observer)
        {
            if (!inventoryObservers.Contains(observer))
            {
                inventoryObservers.Add(observer);
            }
        }

        public void RemoveObserver(IInventoryObserver observer)
        {
            if (inventoryObservers.Contains(observer))
            {
                inventoryObservers.Remove(observer);
            }
        }

        private void NotifyObserversItemAdded(ItemSO item)
        {
            foreach (var observer in inventoryObservers)
            {
                observer.OnItemAddedToInventory(item);
            }
        }

        private void NotifyObserversItemRemoved(ItemSO item)
        {
            foreach (var observer in inventoryObservers)
            {
                observer.OnItemRemovedFromInventory(item);
            }
        }
    }
}


