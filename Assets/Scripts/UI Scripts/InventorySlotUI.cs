using UnityEngine;
using UnityEngine.UIElements;
using Item_Scripts; // Assuming ItemSO is defined in this namespace

public class InventorySlotUI
{
    public VisualElement SlotElement;

    private VisualElement itemIconElement; // Visual element for the item icon
    private Label quantityLabel; // Label for displaying quantity
    private ItemSO currentItem; // Currently displayed item
    private InventoryUIManager UIManager;

    public InventorySlotUI(ItemSO target, InventoryUIManager inventoryUIManager)
    {
        if (!target)
            return;

        UIManager = inventoryUIManager;
        currentItem = target;
        SlotElement = new VisualElement();
        
        SlotElement.RegisterCallback<PointerDownEvent>(OnPointerDown);
        SlotElement.RegisterCallback<PointerUpEvent>(UIManager.OnPointerUp);
        SlotElement.RegisterCallback<PointerMoveEvent>(UIManager.OnPointerMove);
        
        SlotElement.AddToClassList("item-container");

        Background style = SlotElement.style.backgroundImage.value;
        style.sprite = currentItem.ItemIcon;
        SlotElement.style.backgroundImage = style;

        Label quantityLabel = new Label();
        quantityLabel.AddToClassList("item-quantity");
        quantityLabel.text = $"{currentItem.Quantity}";
        SlotElement.Add(quantityLabel);

        
    }

    public void OnDeconstruct()
    {
        SlotElement.UnregisterCallback<PointerDownEvent>(OnPointerDown);
        SlotElement.UnregisterCallback<PointerUpEvent>(UIManager.OnPointerUp);
        SlotElement.UnregisterCallback<PointerMoveEvent>(UIManager.OnPointerMove);
    }


    private void OnPointerDown(PointerDownEvent evt)
    {
        UIManager.OnPointerDown(this, evt);
    }
}