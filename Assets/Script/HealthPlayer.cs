/*
* Description: Controls player health and death.
*/

using UnityEngine;
using TMPro;

/// <summary>
/// Manages player health.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    /// Player's maximum health.
    /// </summary>
    [SerializeField] private float maxHealth = 100f;

    /// <summary>
    /// Health text on UI.
    /// </summary>
    [SerializeField] private TMP_Text healthText;

    /// <summary>
    /// Current health of the player.
    /// </summary>
    private float currentHealth;

    /// <summary>
    /// Checks if the player is dead.
    /// </summary>
    private bool dead;

    /// <summary>
    /// Sets health at the start.
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    /// <summary>
    /// Damages the player.
    /// </summary>
    /// <param name="damage">Damage amount.</param>
    public void TakeDamage(float damage)
    {
        if (dead)
        {
            return;
        }

        currentHealth -= damage;
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

    /// <summary>
    /// Updates health text.
    /// </summary>
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + Mathf.CeilToInt(currentHealth);
        }
    }
}