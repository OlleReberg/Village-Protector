using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject introPanel;
    
    public void NewGame()
    {
        menuPanel.SetActive(false);
        introPanel.SetActive(true);
    }
}
