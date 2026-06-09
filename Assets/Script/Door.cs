/*
* Description: Unlocks the Stage 3 door after 4 Stage 3 coins are collected.
*/

using UnityEngine;

/// <summary>
/// Controls the Stage 3 locked door.
/// </summary>
public class Stage3Door : MonoBehaviour
{
    /// <summary>
    /// The actual door object that will disappear when unlocked.
    /// </summary>
    [SerializeField] private GameObject doorObject;

    /// <summary>
    /// Key used to open the door.
    /// </summary>
    [SerializeField] private KeyCode openKey = KeyCode.E;

    /// <summary>
    /// Checks if the player is near the door.
    /// </summary>
    private bool playerNear;

    /// <summary>
    /// Checks if the door has already opened.
    /// </summary>
    private bool opened;

    /// <summary>
    /// Checks if the player presses E near the door.
    /// </summary>
    private void Update()
    {
        if (playerNear && Input.GetKeyDown(openKey))
        {
            TryOpenDoor();
        }
    }

    /// <summary>
    /// Detects when the player enters the door area.
    /// </summary>
    /// <param name="other">Object that enters the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            playerNear = true;
            Debug.Log("Player is near the door.");

            if (GameManager.instance != null)
            {
                GameManager.instance.ShowMessage("Press E to open door");
            }
        }
    }

    /// <summary>
    /// Detects when the player exits the door area.
    /// </summary>
    /// <param name="other">Object that exits the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            playerNear = false;

            if (GameManager.instance != null)
            {
                GameManager.instance.ClearMessage();
            }
        }
    }

    /// <summary>
    /// Checks if the object is the player.
    /// </summary>
    /// <param name="other">Collider being checked.</param>
    /// <returns>True if the collider belongs to the player.</returns>
    private bool IsPlayer(Collider other)
    {
        return other.CompareTag("Player") || other.GetComponentInParent<PlayerHealth>() != null;
    }

    /// <summary>
    /// Opens the door if the player has collected all 4 Stage 3 coins.
    /// </summary>
    private void TryOpenDoor()
    {
        if (opened)
        {
            return;
        }

        Debug.Log("E pressed near door.");

        if (GameManager.instance != null && GameManager.instance.CanOpenStage3Door())
        {
            opened = true;

            if (doorObject != null)
            {
                doorObject.SetActive(false);
            }

            GameManager.instance.ShowMessage("Door opened!");
            Debug.Log("Door opened.");
        }
        else
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.ShowMessage("Door locked. Collect all 4 Stage 3 coins.");
            }

            Debug.Log("Door still locked.");
        }
    }
}