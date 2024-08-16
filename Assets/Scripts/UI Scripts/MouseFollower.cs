using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    private VisualElement rootElement;
    private VisualElement itemIconElement;
    private Label quantityLabel;

    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        rootElement = uiDocument.rootVisualElement;

        // Assume itemIconElement and quantityLabel are predefined in the UXML
        itemIconElement = rootElement.Q<VisualElement>("ItemIcon");
        quantityLabel = rootElement.Q<Label>("QuantityLabel");
    }

    public void SetData(Sprite sprite, int quantity)
    {
        // Convert the Sprite to a Texture2D and set it as a background
        itemIconElement.style.backgroundImage = new StyleBackground(sprite.texture);
        quantityLabel.text = quantity > 1 ? "x" + quantity : "";
        Toggle(true); // Show the follower
    }

    public void Update()
    {
        // Update position to follow the mouse
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        rootElement.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }

    public void Toggle(bool val)
    {
        rootElement.style.display = val ? DisplayStyle.Flex : DisplayStyle.None;
    }
}


