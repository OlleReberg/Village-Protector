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
    public Text manaText;
    public Image manaBar;
    public PlayerStatsSO playerStatsSO;
    private float health;
    private float maxHealth;
    private float mana;
    private float maxMana;
    private float lerpSpeed;
    public PlayerController playerController;
    private float currentHealthPercent;
    private float currentManaPercent;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        maxMana = playerStatsSO.MagicReserve;
        maxHealth = playerStatsSO.MaxHealth;
    }

    private void Update()
    {
        currentHealthPercent = (100f/maxHealth) * health; //Calculate health percentage
        currentManaPercent = (100f / maxMana) * mana; //Calculate mana percentage
        health = playerController.playerStats.currentHealth;
        mana = playerController.playerStats.currentMana;
        
        healthText.text = "Health: " + currentHealthPercent + "%";
        manaText.text = "Mana: " + currentManaPercent + "%";
        
        if (health > maxHealth)
            health = maxHealth;

        if (mana > maxMana)
            mana = maxMana;
        
        lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
        HealthColorChanger();
        ManaBarFiller();
        ManaColorChanger();
    }

    private void ManaColorChanger()
    {
        Color manaColor = Color.Lerp(Color.cyan, Color.blue, (mana / maxMana));

        manaBar.color = manaColor;
    }

    void HealthBarFiller()
    {
        healthbar.fillAmount = Mathf.Lerp(healthbar.fillAmount, (health/maxHealth), lerpSpeed);
    }

    void ManaBarFiller()
    {
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, (mana / maxMana), lerpSpeed);
    }

    void HealthColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));

        healthbar.color = healthColor;
    }
    
}
