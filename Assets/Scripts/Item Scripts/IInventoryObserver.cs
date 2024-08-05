using UnityEngine;

namespace Item_Scripts
{
    public interface IInventoryObserver
    {
        void OnItemAddedToInventory(ItemSO item);
        void OnItemRemovedFromInventory(ItemSO item);
    }
}