using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Item_Scripts;
using PlayerScripts;

namespace UI_Scripts
{
    public class PlayerInventoryUI : MonoBehaviour, IInventoryObserver
    {
        public PlayerInventory playerInventory;
        private VisualElement rootElement;
        private VisualElement inventoryGrid; // The container for inventory slots
        [SerializeField] private InventoryUIManager uiManager;
        private void Start()
        {
            SetUpUI();
        }

        public void SetUpUI()
        {
            rootElement = GetComponent<UIDocument>().rootVisualElement;
            uiManager.CreateInventorySlots(playerInventory, rootElement);
            //rootElement.Clear();
            playerInventory.AddObserver(this);
           // UpdateUI(); // Initial UI setup
        }

        public void OnItemAddedToInventory(ItemSO item)
        {
            UpdateUI();
        }

        public void OnItemRemovedFromInventory(ItemSO item)
        {
            UpdateUI();
        }

        public void UpdateUI()
        {
            ClearInventoryDisplay();
            foreach (var item in playerInventory.playerInventory)
            {
                CreateItemUI(item);
            }
        }

        private void ClearInventoryDisplay()
        {
            // Clear the existing UI elements in the grid
            rootElement = GetComponent<UIDocument>().rootVisualElement;
            //inventoryGrid = rootElement.Q<VisualElement>("inventoryGrid"); // Ensure this matches the name in your UXML
            //inventoryGrid.Clear();
        }

        private void CreateItemUI(ItemSO item)
        {
            // Create a new VisualElement for the item slot
            var itemElement = new VisualElement();
            itemElement.AddToClassList("inventory-slot");

            // Set the item's icon
            itemElement.style.backgroundImage = new StyleBackground(item.ItemIcon.texture);

            // Create a label for the item quantity
            var quantityLabel = new Label();
            quantityLabel.text = item.Quantity > 1 ? "x" + item.Quantity.ToString() : "";
            quantityLabel.AddToClassList("item-quantity");
            itemElement.Add(quantityLabel);

            // Add the item slot to the grid
            inventoryGrid.Add(itemElement);
        }

        private void OnDestroy()
        {
            if (playerInventory != null)
            {
                playerInventory.RemoveObserver(this);
            }
        }
    }
}
