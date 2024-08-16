using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using PlayerScripts;

public class InventoryUIManager : MonoBehaviour
{
    public bool IsDragging = false;
    private List<InventorySlotUI> allSlotData = new List<InventorySlotUI>();
    private VisualElement inventoryContainer;
    private VisualElement rootElement;
    private VisualElement dragElement;
        
    public void CreateInventorySlots(PlayerInventory playerInventory, VisualElement root)
    {   
        rootElement = root;
        // try get drag object
        dragElement = root.Q<VisualElement>("DragObject");
        if (dragElement == null)
        {
            dragElement = new VisualElement();
            dragElement.name = "DragObject";
            dragElement.AddToClassList("Item-Drag");
            root.Add(dragElement);
        }
        
        dragElement.RegisterCallback<PointerUpEvent>(OnPointerUp);
        dragElement.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        
        var container = rootElement.Q<VisualElement>("InventoryContainer");
        container.Clear();
        int totalSlot = 5;

        foreach (InventorySlotUI slotUI in allSlotData)
        {
            slotUI.OnDeconstruct();
        }
        allSlotData.Clear();
        
        
        int slotIndex = 0;
        int totalVertical = PlayerInventory.TotalSlots / PlayerInventory.TotalHorizontal;
        for (int i = 0; i < PlayerInventory.TotalHorizontal; i++)
        {
            VisualElement horionztalBar = new VisualElement();
            horionztalBar.AddToClassList("inventory-row");
            for (int j = 0; j < totalVertical; j++)
            {
                VisualElement slotRoot = new VisualElement();
                slotRoot.AddToClassList("inventory-slot2");
                horionztalBar.Add(slotRoot);


                if (slotIndex >= 0 && slotIndex < playerInventory.playerInventory.Count)
                {
                    InventorySlotUI slotData = new InventorySlotUI(playerInventory.playerInventory[slotIndex], this);
                    slotRoot.Add(slotData.SlotElement);
                    allSlotData.Add(slotData);
                }
                
                slotIndex++;
            }
            container.Add(horionztalBar);
        }
    }

    public void OnPointerDown(InventorySlotUI dragObject, PointerDownEvent evt)
    {
        switch (evt.button)
        {
            case (int) MouseButton.LeftMouse:
                Debug.Log("Start Drag");  
                dragElement.CapturePointer(evt.pointerId);
                dragElement.style.left = evt.position.x - (dragElement.resolvedStyle.width / 2);
                dragElement.style.top = evt.position.y - (dragElement.resolvedStyle.height / 2);
                dragElement.style.backgroundImage = dragObject.SlotElement.style.backgroundImage;
                dragElement.style.display = DisplayStyle.Flex;
                IsDragging = true;
                break;
            case (int) MouseButton.RightMouse:

                break; 
            default:
                Debug.Log("other mouse button pressed");
                break;
        }
    }
    
    public void OnPointerUp(PointerUpEvent evt)
    {
        switch (evt.button)
        {
            case (int) MouseButton.LeftMouse:
                IsDragging = false;
                dragElement.style.display = DisplayStyle.None;
                dragElement.ReleasePointer(evt.pointerId);
                Debug.Log("Stop Drag");
                break;
            case (int) MouseButton.RightMouse:

                break; 
            default:
                Debug.Log("other mouse button pressed");
                break;
        }
    }
    
    public void OnPointerMove(PointerMoveEvent evt)
    {
        if (IsDragging)
        {
            dragElement.style.left = evt.position.x - (dragElement.resolvedStyle.width / 2);
            dragElement.style.top = evt.position.y - (dragElement.resolvedStyle.height / 2);
        }
    }
}
