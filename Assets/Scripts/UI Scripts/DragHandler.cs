using UnityEngine;
using UnityEngine.UIElements;

// This class manages drag-and-drop interactions for elements within a UI Toolkit environment.
public class DragHandler : MonoBehaviour
{
    private VisualElement draggableElement;  // The element within the UI that will be draggable.
    private Vector2 startMousePosition;      // Stores the initial mouse position at the start of the drag.
    private Vector3 startPosition;           // Stores the initial position of the draggable element.
    private bool isDragging = false;         // Flag to track whether a drag is currently active.

    void Awake()
    {
        // Get the UIDocument component and retrieve the draggable element by querying its name.
        var uiDocument = GetComponent<UIDocument>();
        draggableElement = uiDocument.rootVisualElement.Q<VisualElement>("DraggableElement");

        // Register event callbacks for the drag operations.
        draggableElement.RegisterCallback<PointerDownEvent>(OnPointerDown);
        draggableElement.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        draggableElement.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    // Called when the user presses the pointer down on the draggable element.
    void OnPointerDown(PointerDownEvent evt)
    {
        // Capture the initial positions for later calculation.
        startMousePosition = evt.position;
        startPosition = draggableElement.transform.position;

        // Capture the pointer to ensure all subsequent pointer events are directed to this element.
        draggableElement.CapturePointer(evt.pointerId);
        isDragging = true;  // Set the dragging flag.
    }

    // Called when the pointer moves while being pressed down.
    void OnPointerMove(PointerMoveEvent evt)
    {
        if (isDragging)
        {
            // Manually calculate the movement for each vector component
            float moveX = evt.position.x - startMousePosition.x;
            float moveY = evt.position.y - startMousePosition.y;

            // Apply the calculated movement to the starting position
            draggableElement.style.left = startPosition.x + moveX;
            draggableElement.style.top = startPosition.y + moveY;
        }
    }


    // Called when the user releases the pointer button.
    void OnPointerUp(PointerUpEvent evt)
    {
        // Release the pointer capture and reset the dragging state.
        if (draggableElement.HasPointerCapture(evt.pointerId))
        {
            draggableElement.ReleasePointer(evt.pointerId);
            isDragging = false;
        }
    }
}



