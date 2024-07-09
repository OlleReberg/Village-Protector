using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayerScripts
{
    public class PlayerStats: MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO playerstats;
        public float currentHealth;
        public float currentMana;
        public int attackDamage;
        public int defence;

        public void Awake()
        {
            //Set player stats
            currentHealth = playerstats.MaxHealth;
            currentMana = playerstats.MagicReserve;
            attackDamage = playerstats.AttackDamage;
            defence = playerstats.Defence;
        }
        
        public void ApplyStatsGains(float healthGain, float manaGain, int attackGain, int defenceGain)
        {
            currentHealth += healthGain;
            currentMana += manaGain;
            attackDamage += attackGain;
            defence += defenceGain;
        }

        public void OnBattleVictory(PlayerStatsUI ui)
        {
            float healthGain = Random.Range(5, 15); // Example stat gain
            float manaGain = Random.Range(5, 15);
            int attackGain = Random.Range(1, 5);
            int defenceGain = Random.Range(1, 5);

            ui.ShowStatsGains(healthGain, manaGain, attackGain, defenceGain);
        }
    }
}