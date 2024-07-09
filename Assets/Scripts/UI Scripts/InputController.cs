using PlayerScripts;
using UnityEngine;

namespace UI_Scripts
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private PlayerStatsUI playerStatsUI;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V)) // Simulate a victory with the 'V' key
            {
                playerStats.OnBattleVictory(playerStatsUI);
            }
        }
    }
}