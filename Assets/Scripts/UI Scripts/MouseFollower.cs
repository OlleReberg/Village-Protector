using System;
using System.Collections;
using System.Collections.Generic;
using UI_Scripts;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    

    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        
    }

    public void SetData(Sprite sprite, int quantity)
    {
        
    }

    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform) canvas.transform,
            Input.mousePosition, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        Debug.Log($"selected {val}");
        gameObject.SetActive(val);
    }
}
