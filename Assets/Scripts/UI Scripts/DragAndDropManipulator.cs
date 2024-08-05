using UnityEngine;

namespace UI_Scripts
{
    using UnityEngine.UIElements;

    public class DragAndDropManipulator : MouseManipulator
    {
        private bool m_Active;
        private Vector2 m_Start;
        public DragAndDropManipulator(VisualElement targetElement)
        {
            target = targetElement; // Set the target for the manipulator.
            activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
            RegisterCallbacksOnTarget(); // Registers the callbacks on the targeted element.
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<MouseDownEvent>(OnMouseDown, TrickleDown.TrickleDown);
            target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
            target.RegisterCallback<MouseUpEvent>(OnMouseUp);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<MouseDownEvent>(OnMouseDown, TrickleDown.TrickleDown);
            target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
            target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
        }

        private void OnMouseDown(MouseDownEvent evt)
        {
            if (m_Active)
            {
                evt.StopImmediatePropagation();
                return;
            }

            m_Active = true;
            m_Start = evt.localMousePosition;

            target.CaptureMouse();
            evt.StopPropagation();
        }

        private void OnMouseMove(MouseMoveEvent evt)
        {
            if (!m_Active || !target.HasMouseCapture())
                return;

            var diff = evt.localMousePosition - m_Start;
            (target as VisualElement).transform.position += new Vector3(diff.x, diff.y, 0);
            m_Start = evt.localMousePosition;

            evt.StopPropagation();
        }

        private void OnMouseUp(MouseUpEvent evt)
        {
            if (!m_Active || !target.HasMouseCapture())
                return;

            m_Active = false;
            target.ReleaseMouse();
            evt.StopPropagation();
        }
    }
}