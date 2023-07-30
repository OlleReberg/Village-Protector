using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuToggler : MonoBehaviour
{
    private bool isPaused; // Flag to track if the game is paused or not

    [SerializeField] private GameObject inventoryPanel; // Reference to the inventory panel GameObject
    [SerializeField] private CameraController cameraController; // Reference to the CameraController script

    private void Awake()
    {
        // Deactivate the inventory panel at the start
        inventoryPanel.SetActive(false);

        // Set isPaused to false at the start
        isPaused = false;
    }

    private void Update()
    {
        // Check for the input to toggle the inventory
        ToggleInventory();
    }

    // Method to pause the game
    public void PauseGame()
    {
        // Set the time scale to 0 to pause the game
        Time.timeScale = 0f;

        // Set isPaused to true to indicate that the game is paused
        isPaused = true;

        // Disable the CameraController to prevent player movement during the pause
        cameraController.enabled = false;

        // Log a message to indicate that the game is paused
        Debug.Log("Game should be paused");
    }

    // Method to resume the game
    public void ResumeGame()
    {
        // Set the time scale to 1 to resume the game
        Time.timeScale = 1f;

        // Set isPaused to false to indicate that the game is not paused anymore
        isPaused = false;

        // Enable the CameraController to allow player movement after resuming
        cameraController.enabled = true;

        // Log a message to indicate that the game is unpaused
        Debug.Log("Game should be unpaused");
    }

    // Method to toggle the inventory panel and pause/resume the game accordingly
    public void ToggleInventory()
    {
        // Check if the 'I' key is pressed
        if (Input.GetKeyDown(KeyCode.I))
        {
            // If the game is not paused, pause it and show the inventory panel
            if (!isPaused)
            {
                PauseGame();
                inventoryPanel.SetActive(true);
            }
            // If the game is already paused, resume it and hide the inventory panel
            else if (isPaused)
            {
                ResumeGame();
                inventoryPanel.SetActive(false);
            }
        }
    }
}

