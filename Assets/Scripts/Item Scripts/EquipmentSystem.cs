using UnityEngine;

namespace Item_Scripts
{
    
    public class EquipmentSystem : MonoBehaviour, IInventoryObserver
    {
        // Implement the interface methods to handle inventory changes
        public void OnItemAddedToInventory(ItemSO item)
        {
            // Implement logic to handle item being added to the inventory
        }

        public void OnItemRemovedFromInventory(ItemSO item)
        {
            // Implement logic to handle item being removed from the inventory
        }
    }
    
}