using System;
using System.Collections;
using System.Collections.Generic;
using Item_Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // References to the UI elements in the inventory slot prefab
    public Image itemIconImage;
    public TextMeshProUGUI quantityText;
    
    private TooltipWindow tooltipWindow; // Reference ItemTooltipWindow
    private ItemSO itemSO; //Reference ItemSO
    private PlayerInventory playerInventory; //Reference PlayerInventory

    private void Start()
    {
        Debug.Log("Item Icon Image: " + itemIconImage);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Implement drag logic (e.g., detect item being dragged and show visual feedback)
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Implement dragging logic (e.g., move the item icon along with the cursor)
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Implement drop logic (e.g., check where the item is dropped and take appropriate action)
    }
    // Implement IPointerEnterHandler interface to handle mouse enter event
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Check if the slot has an item
        if (itemSO != null && playerInventory != null && tooltipWindow != null)
        {
            // Show the tooltip window with the item information
            tooltipWindow.ShowTooltip(itemSO);
        }
    }

    // Implement IPointerExitHandler interface to handle mouse exit event
    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the tooltip window when the mouse exits the slot
        if (tooltipWindow != null)
        {
            tooltipWindow.HideTooltip();
        }
    }
}

