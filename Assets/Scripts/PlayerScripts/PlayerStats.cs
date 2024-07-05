using System;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerStats: MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO playerstats;
        public float currentHealth;

        public void Awake()
        {
            //Set player health
            currentHealth = playerstats.MaxHealth;
        }
    }
}