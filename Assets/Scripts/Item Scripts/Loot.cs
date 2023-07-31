using System.Collections;
using System.Collections.Generic;
using Item_Scripts;
using UnityEngine;
using UnityEngine.Events;

public class Loot : MonoBehaviour
{
    public ItemSO item; // The ItemSO representing the pickupable item
    public float pickupRadius = 3f;
    
    private bool canPickup = false;    
    [Header("Events")]
    public UnityEvent onPickup; // Event to invoke when the item is picked up

    private void Update()
    {
        // Check for input to pick up the item
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
        }
    }

    private void Pickup()
    {
        // Invoke the onPickup event to handle the pickup process
        onPickup?.Invoke();

        // Destroy the pickup item GameObject after it has been picked up
        Destroy(gameObject);
    }

    // Optionally, draw the pickup radius in the scene view to visualize it
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}









