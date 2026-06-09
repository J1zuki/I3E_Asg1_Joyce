/*
* Description: Controls score, stage 3 coin count, and UI messages.
*/

using UnityEngine;
using TMPro;

/// <summary>
/// Manages the score, Stage 3 coin count, and game UI.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Allows other scripts to access the GameManager.
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// Text that shows the player's score.
    /// </summary>
    [SerializeField] private TMP_Text scoreText;

    /// <summary>
    /// Text that shows Stage 3 coins collected.
    /// </summary>
    [SerializeField] private TMP_Text stage3CoinText;

    /// <summary>
    /// Text that shows messages to the player.
    /// </summary>
    [SerializeField] private TMP_Text messageText;

    /// <summary>
    /// Panel shown when the player wins.
    /// </summary>
    [SerializeField] private GameObject winPanel;

    /// <summary>
    /// Panel shown when the player loses.
    /// </summary>
    [SerializeField] private GameObject gameOverPanel;

    /// <summary>
    /// Player's current score.
    /// </summary>
    private int score;

    /// <summary>
    /// Number of Stage 3 coins collected.
    /// </summary>
    private int stage3Coins;

    /// <summary>
    /// Number of Stage 3 coins needed to unlock the door.
    /// </summary>
    [SerializeField] private int stage3CoinsNeeded = 4;

    /// <summary>
    /// Sets up the GameManager.
    /// </summary>
    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Updates UI at the start.
    /// </summary>
    private void Start()
    {
        UpdateScoreUI();
        UpdateStage3CoinUI();
        ShowMessage("Collect coins and reach the end!");
    }

    /// <summary>
    /// Adds score when a coin is collected.
    /// </summary>
    /// <param name="amount">Amount of score to add.</param>
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    /// <summary>
    /// Adds one Stage 3 coin.
    /// </summary>
    public void AddStage3Coin()
    {
        stage3Coins++;
        UpdateStage3CoinUI();

        if (stage3Coins >= stage3CoinsNeeded)
        {
            ShowMessage("All Stage 3 coins collected. Door unlocked!");
        }
    }

    /// <summary>
    /// Checks if the Stage 3 door can open.
    /// </summary>
    /// <returns>True if enough Stage 3 coins are collected.</returns>
    public bool CanOpenStage3Door()
    {
        return stage3Coins >= stage3CoinsNeeded;
    }

    /// <summary>
    /// Shows a message on screen.
    /// </summary>
    /// <param name="message">Message to show.</param>
    public void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    /// <summary>
    /// Clears the message text.
    /// </summary>
    public void ClearMessage()
    {
        if (messageText != null)
        {
            messageText.text = "";
        }
    }

    /// <summary>
    /// Shows the win screen.
    /// </summary>
    public void WinGame()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        ShowMessage("You reached the finish line!");
    }

    /// <summary>
    /// Shows the game over screen.
    /// </summary>
    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        ShowMessage("Game Over!");
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Updates the score UI.
    /// </summary>
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    /// <summary>
    /// Updates the Stage 3 coin UI.
    /// </summary>
    private void UpdateStage3CoinUI()
    {
        if (stage3CoinText != null)
        {
            stage3CoinText.text = "Stage 3 Coins: " + stage3Coins + " / " + stage3CoinsNeeded;
        }
    }
}