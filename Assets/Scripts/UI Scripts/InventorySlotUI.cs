using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
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

