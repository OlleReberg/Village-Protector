using PlayerScripts;
using UnityEngine;

namespace Item_Scripts
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Consumable")]
    public class ConsumableItemSO : ItemSO, IConsumable
    {
        public enum EffectType { Health, Mana, PhysicalResistance }
        [SerializeField] private EffectType effectType;
        public float effectPower;
        public float effectDuration; // Use for timed effects like resistance.

        public void Consume(PlayerStats playerStats)
        {
            switch (effectType)
            {
                case EffectType.Health:
                    playerStats.currentHealth += effectPower;
                    playerStats.currentHealth = Mathf.Clamp(playerStats.currentHealth, 0, playerStats.maxHealth);
                    break;
                case EffectType.Mana:
                    playerStats.currentMana += effectPower;
                    playerStats.currentMana = Mathf.Clamp(playerStats.currentMana, 0, playerStats.playerstatsSO.MagicReserve);
                    break;
                case EffectType.PhysicalResistance:
                    // Apply resistance logic here
                    break;
            }
            Debug.Log($"Consumed: {name}, Effect: {effectType}, Power: {effectPower}");
        }
    }
}