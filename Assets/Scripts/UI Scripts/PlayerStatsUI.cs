using PlayerScripts;
using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats; // Reference to the PlayerStats component
    [SerializeField] private GameObject statsPanel; // Reference to the UI panel that displays stats
    [SerializeField] private TextMeshProUGUI statsText; // Reference to the TextMeshProUGUI component for displaying stats
    [SerializeField] private TextMeshProUGUI statsGainsText; // Reference to the TextMeshProUGUI component for displaying stat gains

    private bool gainsDisplayed = false; // Flag to check if gains are being displayed
    private bool isStatsWindowVisible = false; // Flag to check if the stats window is currently visible

    private void Start()
    {
        // Check if the playerStats reference is assigned
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats reference is null.");
            return;
        }

        // Hide the stats panel at the start
        statsPanel.SetActive(false);

        // Initialize the UI with the current stats
        UpdateStatsUI();
    }

    private void Update()
    {
        // Check for 'X' key press to apply gains or toggle the stats window
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (gainsDisplayed)
            {
                // Apply the gains and prepare to close the window on next press
                ApplyStatsGains();
                gainsDisplayed = false;
            }
            else if (isStatsWindowVisible)
            {
                // Hide the stats window
                ToggleStatsWindow(false);
            }
        }
    }

    public void UpdateStatsUI()
    {
        // Update the stats text with current stats
        statsText.text = $"HP: {playerStats.currentHealth}\nMP: {playerStats.currentMana}\nAtt: {playerStats.attackDamage}\nDef: {playerStats.defence}";
    }

    public void ShowStatsGains(float healthGain, float manaGain, float attackGain, float defenceGain)
    {
        // Show the stat gains in the statsGainsText
        statsGainsText.text = $"+{healthGain}\n+{manaGain}\n+{attackGain}\n+{defenceGain}";
        gainsDisplayed = true; // Indicate that the gains are being displayed

        // Show the stats window
        ToggleStatsWindow(true);
    }

    public void ApplyStatsGains()
    {
        // Extract the gain values from the statsGainsText
        string[] lines = statsGainsText.text.Split('\n');
        float healthGain = float.Parse(lines[0].Trim().Split('+')[1]);
        float manaGain = float.Parse(lines[1].Trim().Split('+')[1]);
        int attackGain = int.Parse(lines[2].Trim().Split('+')[1]);
        int defenceGain = int.Parse(lines[3].Trim().Split('+')[1]);

        // Apply the gains to the player stats
        playerStats.ApplyStatsGains(healthGain, manaGain, attackGain, defenceGain, combatRatingGain: 1);

        // Update the stats UI
        UpdateStatsUI();

        // Clear the gains text
        statsGainsText.text = "";
    }

    public void ToggleStatsWindow(bool show)
    {
        // Toggle the visibility of the stats window
        statsPanel.SetActive(show);
        isStatsWindowVisible = show;
    }
}


