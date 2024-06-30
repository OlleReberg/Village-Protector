using System.Collections;
using System.Collections.Generic;
using Item_Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipWindow : MonoBehaviour
{
    public RectTransform contentPanel;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public Image itemIconImage;

    private RectTransform rectTransform;
    private bool isTooltipActive;
    
    public delegate void ItemHoveredEventHandler(ItemSO item);
    public static event ItemHoveredEventHandler OnItemHovered;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        HideTooltip();
    }

    public void ShowTooltip(ItemSO item)
    {
        itemNameText.text = item.ItemName;
        itemDescriptionText.text = item.ItemDescription;
        itemIconImage.sprite = item.ItemIcon;

        // Update the layout of the content panel to fit the content
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentPanel);

        // Resize the tooltip window to fit the content
        Vector2 contentSize = contentPanel.sizeDelta;
        Vector2 windowSize = new Vector2(contentSize.x, contentSize.y + 20f); // Add a small padding
        rectTransform.sizeDelta = windowSize;

        // Move the tooltip to the mouse position
        rectTransform.position = Input.mousePosition;

        // Show the tooltip
        gameObject.SetActive(true);
        isTooltipActive = true;
        
        // Trigger the OnItemHovered event
        if (OnItemHovered != null)
        {
            OnItemHovered(item);
            Debug.Log("Hovering " + item.ItemName);
        }
    }

    public void HideTooltip()
    {
        // Hide the tooltip
        gameObject.SetActive(false);
        isTooltipActive = false;
    }

    private void Update()
    {
        // If the tooltip is active, move it with the mouse
        if (isTooltipActive)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}
