/*
* Description: Controls score, stage 3 coin count, UI messages, win screen, game over screen, and restart.
*/

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// Manages score, Stage 3 coins, UI, win screen, game over screen, and restart.
public class GameManager : MonoBehaviour
{
    /// Allows other scripts to access the GameManager.
    public static GameManager instance;

    /// Text that shows the player's score.
    [SerializeField] private TMP_Text scoreText;

    /// Text that shows the Stage 3 coin progress.
    [SerializeField] private TMP_Text stage3CoinText;

    /// Text that shows small messages to the player.
    [SerializeField] private TMP_Text messageText;

    /// Text that shows the final score on the win screen.
    [SerializeField] private TMP_Text finalScoreText;

    /// Panel that appears when the player wins.
    [SerializeField] private GameObject winPanel;

    /// Panel that appears when the player dies.
    [SerializeField] private GameObject gameOverPanel;

    /// Player's current score.
    private int score;

    /// Number of Stage 3 coins collected.
    private int stage3Coins;

    /// Number of Stage 3 coins needed to unlock the door.
    [SerializeField] private int stage3CoinsNeeded = 4;

    /// Sets up the GameManager when the game starts.
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

    /// Updates the UI at the start of the game.
    private void Start()
    {
        UpdateScoreUI();
        UpdateStage3CoinUI();
        ShowMessage("Collect coins and reach the finish line!");
    }

    /// Adds score when the player collects a coin.
    /// <param name="amount">Amount of score to add.</param>
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    /// Adds one Stage 3 coin to the count.
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

    /// Checks whether the Stage 3 door can open.
    /// <returns>True if all 4 Stage 3 coins are collected.</returns>
    public bool CanOpenStage3Door()
    {
        return stage3Coins >= stage3CoinsNeeded;
    }

    /// Shows a message on the screen.
    /// <param name="message">Message to show.</param>
    public void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    /// Clears the message text.
    public void ClearMessage()
    {
        if (messageText != null)
        {
            messageText.text = "";
        }
    }

    /// Shows the win screen when the player reaches the finish line.
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

    /// Shows the game over screen when the player dies.
    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        ShowMessage("Game Over!");
        Time.timeScale = 0f;
    }

    /// Restarts the current scene.
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// Updates the score text.
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    /// Updates the Stage 3 coin text.
    private void UpdateStage3CoinUI()
    {
        if (stage3CoinText != null)
        {
            stage3CoinText.text = "Stage 3 Coins: " + stage3Coins + " / " + stage3CoinsNeeded;
        }
    }
}