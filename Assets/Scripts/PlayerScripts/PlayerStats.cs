using System;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerStats: MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO playerstats;
        public float currentHealth;
        public float currentMana;

        public void Awake()
        {
            //Set player health & mana
            currentHealth = playerstats.MaxHealth;
            currentMana = playerstats.MagicReserve;
        }
    }
}