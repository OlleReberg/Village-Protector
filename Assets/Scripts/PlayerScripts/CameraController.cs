using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followPlayer;   // Player to follow
    [SerializeField] private float distance = 5;   // Distance of camera from player
    [SerializeField] private float rotationSpeed = 2f;   // Speed of camera rotation
    [SerializeField] private float minVerticalAngle = -45;   // Minimum vertical angle of camera
    [SerializeField] private float maxVerticalAngle = 45;   // Maximum vertical angle of camera
    [SerializeField] private Vector2 framingOffset;   // Offset for camera's focus position

    [SerializeField] private bool invertX;   // Option to invert horizontal rotation
    [SerializeField] private bool invertY;   // Option to invert vertical rotation
    
    private float rotationY;   // Current horizontal rotation of camera
    private float rotationX;   // Current vertical rotation of camera
    private float invertXVal;   // Value to multiply horizontal rotation by to invert it
    private float invertYVal;   // Value to multiply vertical rotation by to invert it

    private void Start()
    {
        // Hide cursor and lock it to the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //If one wants inverted rotation,
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

    // Get the camera's planar rotation (horizontal rotation)
    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}
