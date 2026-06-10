/*
* Description: Controls player health and death.
*/

using UnityEngine;
using TMPro;

/// Manages player health.
public class PlayerHealth : MonoBehaviour
{
    /// Player's maximum health.
    [SerializeField] private float maxHealth = 100f;

    /// Health text on UI.
    [SerializeField] private TextMeshProUGUI healthText;

    /// Current health of the player.
    private float currentHealth;

    /// Checks if the player is dead.
    private bool dead;

    /// Sets health at the start.
    private void Start()
    {
        currentHealth = maxHealth;
        dead = false;

        UpdateHealthUI();
    }

    /// Damages the player.
    public void TakeDamage(float damage)
    {
        if (dead)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth < 0f)
        {
            currentHealth = 0f;
        }

        UpdateHealthUI();

        if (currentHealth <= 0f)
        {
            dead = true;

            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
        }
    }

    /// Updates health text.
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + Mathf.CeilToInt(currentHealth);
        }
    }
}