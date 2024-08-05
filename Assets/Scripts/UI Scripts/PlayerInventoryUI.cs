using Item_Scripts;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI_Scripts
{
    public class PlayerInventoryUI : MonoBehaviour
    {
        public PlayerInventory playerInventory;
        private VisualElement rootElement;

        void Start()
        {
            rootElement = GetComponent<UIDocument>().rootVisualElement;
            SetupDragAndDrop();
        }

        private void SetupDragAndDrop()
        {
            foreach (var itemVisual in rootElement.Query<VisualElement>().Where(e => e.userData is ItemSO).ToList())
            {
                var manipulator = new DragAndDropManipulator(itemVisual);
                itemVisual.AddManipulator(manipulator);
            }
        }
        public void UpdateUI()
        {
            // Clear existing UI elements
            ClearInventoryDisplay();

            // Create new UI elements based on PlayerInventory's items
            foreach (var item in playerInventory.playerInventory)
            {
                CreateItemUI(item);
            }
        }

        void ClearInventoryDisplay()
        {
            /* Implementation */
        }

        void CreateItemUI(ItemSO item)
        {
            /* Implementation */
        }
    }
}