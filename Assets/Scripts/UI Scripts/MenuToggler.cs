using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuToggler : MonoBehaviour
{
   private bool isPaused;
   [SerializeField] private GameObject inventoryPanel;
   [SerializeField] private CameraController cameraController;

   private void Awake()
   {
      inventoryPanel.SetActive(false);
      isPaused = false;
   }

   private void Update()
   {
      ToggleInventory();
   }

   public void PauseGame()
   {
      Time.timeScale = 0f;
      isPaused = true;
      Debug.Log("Game should be paused");
      cameraController.enabled = false;

   }

   public void ResumeGame()
   {
      Time.timeScale = 1f;
      isPaused = false;
      Debug.Log("Game should be unpaused");
      cameraController.enabled = true;
   }

   public void ToggleInventory()
   {
      if (Input.GetKeyDown(KeyCode.I))
      {
         if (!isPaused)
         {
            PauseGame();
            inventoryPanel.SetActive(true);
         }
         else if (isPaused)
         {
            ResumeGame();
            inventoryPanel.SetActive(false);
         }
      }
   }
}
