using System;
using System.Collections;
using System.Collections.Generic;
using Item_Scripts;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemSO> playerInventory = new List<ItemSO>();
    private List<IInventoryObserver> inventoryObservers = new List<IInventoryObserver>();
    public GameObject inventoryPanel;
    public GameObject inventorySlotPrefab;
    private WeaponSlotManager weaponSlotManager;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;

    private void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        UpdateUI();
    }

    private void Start()
    {
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
    }

    private void Update()
    {
        //UpdateUI();
    }

    public void AddItemToInventory(ItemSO item)
    {
        playerInventory.Add(item); // Notify observers (other systems) that an item has been added.

        // Update the UI
        GameObject newSlot = Instantiate(inventorySlotPrefab, 
            inventoryPanel.transform); // Set the item icon and other data in the newSlot UI using 'item'
        
        InventorySlotUI slotUI = newSlot.GetComponent<InventorySlotUI>();

        // Set the item icon and quantity in the newSlot UI using 'item'
        slotUI.itemIconImage.sprite = item.ItemIcon;
        slotUI.quantityText.text = "x" + item.Quantity.ToString();
        
        // Adjust image aspect ratio to fit within the UISlot
        AdjustImageAspect(slotUI.itemIconImage);
    }

    public void RemoveItemFromInventory(ItemSO item)
    {
        playerInventory.Remove(item);
        // Notify observers (other systems) that an item has been removed.
        NotifyObserversItemRemoved(item);

        // Update the UI after removing the item
        UpdateUI();
    }
    
    public void AddObserver(IInventoryObserver observer)
    {
        inventoryObservers.Add(observer);
    }

    public void RemoveObserver(IInventoryObserver observer)
    {
        inventoryObservers.Remove(observer);
    }
    
    private void NotifyObserversItemAdded(ItemSO item)
    {
        foreach (IInventoryObserver observer in inventoryObservers)
        {
            observer.OnItemAddedToInventory(item);
        }
    }

    private void NotifyObserversItemRemoved(ItemSO item)
    {
        foreach (IInventoryObserver observer in inventoryObservers)
        {
            observer.OnItemRemovedFromInventory(item);
        }
    }
    
    private void UpdateUI()
    {
        // Clear existing slots in the UI
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate inventory slots for each item in the playerInventory
        foreach (ItemSO item in playerInventory)
        {
            // Instantiate the inventory slot prefab only once
            GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryPanel.transform);
            InventorySlotUI slotUI = newSlot.GetComponent<InventorySlotUI>();

            // Set the item icon and quantity in the newSlot UI using 'item'
            slotUI.itemIconImage.sprite = item.ItemIcon;
            slotUI.quantityText.text = "x" + item.Quantity.ToString();
            
            // Adjust image aspect ratio to fit within the UISlot
            AdjustImageAspect(slotUI.itemIconImage);
        }
    }
    private void AdjustImageAspect(Image image)
    {
        // Get the Image's RectTransform
        RectTransform imageRectTransform = image.GetComponent<RectTransform>();

        // Get the aspect ratio of the sprite
        float aspectRatio = (float)image.sprite.texture.width / image.sprite.texture.height;

        // Match the Image's width and height to the UISlot's dimensions
        imageRectTransform.sizeDelta = new Vector2(imageRectTransform.sizeDelta.y * aspectRatio, imageRectTransform.sizeDelta.y);

        // Set preserveAspect to true programmatically
        image.preserveAspect = true;
    }
    
    public void PickupLoot(ItemSO item)
    {
        // Perform any actions related to picking up the loot (e.g., displaying loot acquisition message, playing sound, etc.)

        // Add the item to the player's inventory
        AddItemToInventory(item);
    }
}
