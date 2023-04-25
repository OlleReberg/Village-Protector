using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] private Transform orientation;
    private float horizontal;
    private float vertical;
    private float moveSpeed;
    private Vector3 moveDirection;
    
    void Start()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = orientation.forward * horizontal + orientation.right * horizontal;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }
}
