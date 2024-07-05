using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text healthText;
    public Image healthbar;
    public PlayerStats playerStats;
    public PlayerStatsSO playerStatsSO;
    private float health;
    private float maxHealth;
    private float lerpSpeed;

    private void Start()
    {
        health = playerStats.currentHealth;
        maxHealth = playerStatsSO.MaxHealth;
    }

    private void Update()
    {
        
        healthText.text = "Health: " + health + "%";
        if (health > maxHealth)
            health = maxHealth;

        lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
        ColorChanger();
    }

    void HealthBarFiller()
    {
        healthbar.fillAmount = Mathf.Lerp(healthbar.fillAmount, (health/maxHealth), lerpSpeed);
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));

        healthbar.color = healthColor;
    }

    public void Damage(float damagePoints)
    {
        if (health > 0)
            health -= damagePoints;
    }

    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
            health += healingPoints;
    }
}
