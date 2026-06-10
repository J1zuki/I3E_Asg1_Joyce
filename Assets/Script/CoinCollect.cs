/*
* Description: Allows the player to collect coins by pressing E.
*/

using UnityEngine;

/// Handles coin collection using the interact key.
public class CoinCollect : MonoBehaviour
{
    /// Score given by this coin.
    [SerializeField] private int scoreValue = 1;

    /// Checks if this coin belongs to Stage 3.
    [SerializeField] private bool isStage3Coin;

    /// Key used to collect the coin.
    [SerializeField] private KeyCode collectKey = KeyCode.E;

    /// AudioSource used to play the coin collection sound.
    private AudioSource collectAudio;

    /// Checks if the player is near the coin.
    private bool playerNear;

    /// Checks if the coin has already been collected.
    private bool collected;

    /// Gets the AudioSource component when the game starts.
    private void Start()
    {
        collectAudio = GetComponent<AudioSource>();
    }

    /// Checks for player input.
    private void Update()
    {
        if (playerNear && Input.GetKeyDown(collectKey) && !collected)
        {
            CollectCoin();
        }
    }

    /// Detects when the player enters the coin trigger.
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

    /// Detects when the player leaves the coin trigger.
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

    /// Adds score and removes the coin.
    private void CollectCoin()
    {
        collected = true;
        playerNear = false;

        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(scoreValue);

            if (isStage3Coin)
            {
                GameManager.instance.AddStage3Coin();
            }

            GameManager.instance.ClearMessage();
        }

        if (collectAudio != null)
        {
            collectAudio.Play();

            MeshRenderer coinRenderer = GetComponent<MeshRenderer>();
            Collider coinCollider = GetComponent<Collider>();

            if (coinRenderer != null)
            {
                coinRenderer.enabled = false;
            }

            if (coinCollider != null)
            {
                coinCollider.enabled = false;
            }

            if (collectAudio.clip != null)
            {
                Destroy(gameObject, collectAudio.clip.length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}