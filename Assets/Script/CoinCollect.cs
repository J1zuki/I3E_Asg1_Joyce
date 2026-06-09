/*
* Author: Your Name
* Date: 2026
* Description: Allows the player to collect coins by pressing E.
*/

using UnityEngine;

/// <summary>
/// Handles coin collection using the interact key.
/// </summary>
public class CoinCollect : MonoBehaviour
{
    /// <summary>
    /// Score given by this coin.
    /// </summary>
    [SerializeField] private int scoreValue = 1;

    /// <summary>
    /// Checks if this coin belongs to Stage 3.
    /// </summary>
    [SerializeField] private bool isStage3Coin;

    /// <summary>
    /// Key used to collect the coin.
    /// </summary>
    [SerializeField] private KeyCode collectKey = KeyCode.E;

    /// <summary>
    /// Sound played when the coin is collected.
    /// </summary>
    [SerializeField] private AudioClip collectSound;

    /// <summary>
    /// Checks if the player is near the coin.
    /// </summary>
    private bool playerNear;

    /// <summary>
    /// Checks if the coin has already been collected.
    /// </summary>
    private bool collected;

    /// <summary>
    /// Checks for player input.
    /// </summary>
    private void Update()
    {
        if (playerNear && Input.GetKeyDown(collectKey) && !collected)
        {
            CollectCoin();
        }
    }

    /// <summary>
    /// Detects when the player enters the coin trigger.
    /// </summary>
    /// <param name="other">Object that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponentInParent<PlayerHealth>() != null)
        {
            playerNear = true;

            if (GameManager.instance != null)
            {
                GameManager.instance.ShowMessage("Press E to collect coin");
            }
        }
    }

    /// <summary>
    /// Detects when the player leaves the coin trigger.
    /// </summary>
    /// <param name="other">Object that exited the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponentInParent<PlayerHealth>() != null)
        {
            playerNear = false;

            if (GameManager.instance != null)
            {
                GameManager.instance.ClearMessage();
            }
        }
    }

    /// <summary>
    /// Adds score and removes the coin.
    /// </summary>
    private void CollectCoin()
    {
        collected = true;

        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(scoreValue);

            if (isStage3Coin)
            {
                GameManager.instance.AddStage3Coin();
            }
        }

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        Destroy(gameObject);
    }
}