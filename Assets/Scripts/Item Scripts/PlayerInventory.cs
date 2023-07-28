using System;
using System.Collections;
using System.Collections.Generic;
using Item_Scripts;
using UnityEngine;

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
        
        NotifyObserversItemAdded(item);
    }

    public void RemoveItemFromInventory(ItemSO item)
    {
        playerInventory.Remove(item);
        // Notify observers (other systems) that an item has been removed.
        NotifyObserversItemRemoved(item);
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
        }
    }

}
