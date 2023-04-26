using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float rotationSpeed = 500f;

    private Quaternion targetRotation;

    private CameraController cameraController;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v= Input.GetAxis("Vertical");

        float moveAmount = Mathf.Abs(h) + Mathf.Abs(v);
        
        var moveInput = new Vector3(h, 0, v).normalized;

        var moveDir = cameraController.PlanarRotation * moveInput;

        if (moveAmount > 0)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, 
            targetRotation, rotationSpeed * Time.deltaTime);

    }
}
