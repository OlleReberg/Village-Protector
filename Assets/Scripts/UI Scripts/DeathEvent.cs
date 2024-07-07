using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathEvent : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel; //fetching death panel
    public float fadeDuration = 2.0f; // Duration of the fade effect in seconds
    public TextMeshProUGUI deathText;
    [SerializeField] private Image deathPanelImage; //grab panel image
    public PlayerStats playerStats; // Get player stats

    private void Start()
    {
        //initialize alpha at 0
        SetAlpha(0);
    }

    private void Update()
    {
        if (playerStats.currentHealth == 0)
        {
            TriggerDeath();
        }
    }

    private void SetAlpha(float alpha)
    {
        Color panelColor = deathPanelImage.color;
        panelColor.a = alpha;
        deathPanelImage.color = panelColor;

        Color textColor = deathText.color;
        textColor.a = alpha;
        deathText.color = textColor;

    }
    // Coroutine to fade in the alpha value
    public IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            SetAlpha(alpha);
            yield return new WaitForEndOfFrame(); // Use WaitForEndOfFrame for more consistent frame timing
        }
        // Ensure the alpha is fully set to 1 after the fade duration
        SetAlpha(1.0f);
    }
    public void TriggerDeath()
    {
        deathPanel.SetActive(true);
        StartCoroutine(FadeIn());
    }
    
}
