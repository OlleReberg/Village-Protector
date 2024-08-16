using UnityEngine;
using UnityEngine.UIElements;
using Item_Scripts;

public class TooltipWindow : MonoBehaviour
{
    private VisualElement tooltipElement;
    private Label itemNameLabel;
    private Label itemDescriptionLabel;
    private VisualElement itemIconElement;

    void Awake()
    {
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        // Ensure names match your UXML file
        tooltipElement = root.Q<VisualElement>("Tooltip");
        itemNameLabel = tooltipElement?.Q<Label>("ItemNameLabel");
        itemDescriptionLabel = tooltipElement?.Q<Label>("ItemDescriptionLabel");
        itemIconElement = tooltipElement?.Q<VisualElement>("ItemIconElement");

        if (tooltipElement == null || itemNameLabel == null || itemDescriptionLabel == null || itemIconElement == null)
        {
            Debug.LogError("TooltipWindow: Failed to find one or more UI elements. Please check the UXML file and element names.");
            return;
        }

        HideTooltip();  // Start hidden
    }

    public void ShowTooltip(ItemSO item)
    {
        if (tooltipElement == null) return;

        itemNameLabel.text = item.ItemName;
        itemDescriptionLabel.text = item.ItemDescription;
        if (item.ItemIcon != null)
        {
            itemIconElement.style.backgroundImage = new StyleBackground(item.ItemIcon);
        }
        tooltipElement.style.display = DisplayStyle.Flex;  // Show the tooltip
    }

    public void HideTooltip()
    {
        if (tooltipElement != null)
        {
            tooltipElement.style.display = DisplayStyle.None;  // Hide the tooltip
        }
    }

    public void UpdateTooltipPosition(Vector2 position)
    {
        if (tooltipElement == null) return;

        tooltipElement.style.left = position.x;
        tooltipElement.style.top = position.y;
    }
}



