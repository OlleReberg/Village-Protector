using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followPlayer;
    [SerializeField] private float distance = 5;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float minVerticalAngle = -45;
    [SerializeField] private float maxVerticalAngle = 45;
    [SerializeField] private Vector2 framingOffset;

    [SerializeField] private bool invertX;
    [SerializeField] private bool invertY;
    
    private float rotationY;
    private float rotationX;
    private float invertXVal;
    private float invertYVal;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //If one wants inverted rotation, simply set true in inspector
        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;
        
        //Using mouse movement/input to determine camera rotation and determine its rotationspeed
        rotationY += Input.GetAxis("Mouse X") * invertXVal * rotationSpeed;
        rotationX += Input.GetAxis("Mouse Y") * invertYVal * rotationSpeed;
        
        //clamp vertical rotation to prevent going too high or low
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
        
        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        //To focus cameras position around the top of player
        var focusPosition = followPlayer.position + new Vector3(framingOffset.x, framingOffset.y);
        
        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
    }
}
