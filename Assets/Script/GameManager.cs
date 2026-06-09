/*
* Author: Your Name
* Date: 2026
* Description: Controls score, stage 3 coin count, UI messages, win screen, and game over screen.
*/

using UnityEngine;
using TMPro;

/// <summary>
/// Manages score, Stage 3 coins, UI, win screen, and game over screen.
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
    /// Text that shows the Stage 3 coin progress.
    /// </summary>
    [SerializeField] private TMP_Text stage3CoinText;

    /// <summary>
    /// Text that shows small messages to the player.
    /// </summary>
    [SerializeField] private TMP_Text messageText;

    /// <summary>
    /// Text that shows the final score on the win screen.
    /// </summary>
    [SerializeField] private TMP_Text finalScoreText;

    /// <summary>
    /// Panel that appears when the player wins.
    /// </summary>
    [SerializeField] private GameObject winPanel;

    /// <summary>
    /// Panel that appears when the player dies.
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
    /// Sets up the GameManager when the game starts.
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
    /// Updates the UI at the start of the game.
    /// </summary>
    private void Start()
    {
        UpdateScoreUI();
        UpdateStage3CoinUI();
        ShowMessage("Collect coins and reach the finish line!");
    }

    /// <summary>
    /// Adds score when the player collects a coin.
    /// </summary>
    /// <param name="amount">Amount of score to add.</param>
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    /// <summary>
    /// Adds one Stage 3 coin to the count.
    /// </summary>
    public void AddStage3Coin()
    {
        stage3Coins++;
        UpdateStage3CoinUI();

        if (stage3Coins >= stage3CoinsNeeded)
        {
            ShowMessage("All Stage 3 coins collected. Door unlocked!");
        }
        else
        {
            ShowMessage("Stage 3 coin collected!");
        }
    }

    /// <summary>
    /// Checks whether the Stage 3 door can open.
    /// </summary>
    /// <returns>True if all 4 Stage 3 coins are collected.</returns>
    public bool CanOpenStage3Door()
    {
        return stage3Coins >= stage3CoinsNeeded;
    }

    /// <summary>
    /// Shows a message on the screen.
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
    /// Shows the win screen when the player reaches the finish line.
    /// </summary>
    public void WinGame()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "You reached the finish line!\nTotal Score: " + score;
        }

        ShowMessage("You reached the finish line!");
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Shows the game over screen when the player dies.
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
    /// Updates the score text.
    /// </summary>
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    /// <summary>
    /// Updates the Stage 3 coin text.
    /// </summary>
    private void UpdateStage3CoinUI()
    {
        if (stage3CoinText != null)
        {
            stage3CoinText.text = "Stage 3 Coins: " + stage3Coins + " / " + stage3CoinsNeeded;
        }
    }
}