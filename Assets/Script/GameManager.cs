/*
* Description: Controls score, stage 3 coin count, UI messages, win screen, game over screen, and restart.
*/

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// Manages the game score, Stage 3 coin count, UI messages, win screen, game over screen, and restart.
public class GameManager : MonoBehaviour
{
    /// Allows other scripts to access the GameManager.
    public static GameManager instance;

    /// Text that shows the player's score.
    [SerializeField] private TextMeshProUGUI scoreText;

    /// Text that shows the Stage 3 coin progress.
    [SerializeField] private TextMeshProUGUI stage3CoinText;

    /// Text that shows small messages to the player.
    [SerializeField] private TextMeshProUGUI messageText;

    /// Text that shows the final score on the win screen.
    [SerializeField] private TextMeshProUGUI finalScoreText;

    /// Panel that appears when the player wins.
    [SerializeField] private GameObject winPanel;

    /// Panel that appears when the player dies.
    [SerializeField] private GameObject gameOverPanel;

    /// Number of Stage 3 coins needed to unlock the door.
    [SerializeField] private int stage3CoinsNeeded = 4;

    /// Player's current score.
    private int score;

    /// Number of Stage 3 coins collected.
    private int stage3Coins;

    /// Checks if the game has ended.
    private bool gameEnded;

    /// Sets up the GameManager before the game starts.
    private void Awake()
    {
        instance = this;

        score = 0;
        stage3Coins = 0;
        gameEnded = false;

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetPanelState(winPanel, false);
        SetPanelState(gameOverPanel, false);
    }

    /// Updates the UI at the start of the game.
    private void Start()
    {
        UpdateScoreUI();
        UpdateStage3CoinUI();
        ShowMessage("Collect coins and reach the finish line!");
    }

    /// Checks for restart input after the game ends.
    private void Update()
    {
        if (gameEnded && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    /// Adds score when the player collects a coin.
    /// <param name="amount">The amount of score to add.</param>
    public void AddScore(int amount)
    {
        if (gameEnded)
        {
            return;
        }

        score += amount;
        UpdateScoreUI();
    }

    /// Adds one Stage 3 coin to the count.
    public void AddStage3Coin()
    {
        if (gameEnded)
        {
            return;
        }

        stage3Coins++;
        UpdateStage3CoinUI();

        if (CanOpenStage3Door())
        {
            ShowMessage("All Stage 3 coins collected. Door unlocked!");
        }
        else
        {
            ShowMessage("Stage 3 coin collected!");
        }
    }

    /// Checks whether the Stage 3 door can open.
    /// <returns>True if enough Stage 3 coins have been collected.</returns>
    public bool CanOpenStage3Door()
    {
        return stage3Coins >= stage3CoinsNeeded;
    }

    /// Shows a message on the screen.
    /// <param name="message">The message to show.</param>
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
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        SetPanelState(winPanel, true);

        if (finalScoreText != null)
        {
            finalScoreText.text = "You reached the finish line!\nTotal Score: " + score + "\nPress R to restart";
        }

        ShowMessage("You reached the finish line!");
        UnlockCursor();
        Time.timeScale = 0f;
    }

    /// Shows the game over screen when the player dies.
    public void GameOver()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        SetPanelState(gameOverPanel, true);
        ShowMessage("Game Over! Press R to restart");

        UnlockCursor();
        Time.timeScale = 0f;
    }

    /// Restarts the current scene.
    public void RestartGame()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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

    /// Turns a UI panel on or off.
    /// <param name="panel">The panel to update.</param>
    /// <param name="isActive">Whether the panel should be active.</param>
    private void SetPanelState(GameObject panel, bool isActive)
    {
        if (panel != null)
        {
            panel.SetActive(isActive);
        }
    }

    /// Unlocks and shows the cursor when the game ends.
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}