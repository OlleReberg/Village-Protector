using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // References to the UI elements in the inventory slot prefab
    public Image itemIconImage;
    public TextMeshProUGUI quantityText;

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
}

