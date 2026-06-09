/*
* Author: Your Name
* Date: 2026
* Description: Opens the Stage 3 door with E after 4 coins, and closes it when player walks away.
*/

using UnityEngine;

/// <summary>
/// Controls the Stage 3 door using animation triggers.
/// </summary>
public class Stage3Door : MonoBehaviour
{
    /// <summary>
    /// Animator attached to the door object.
    /// </summary>
    [SerializeField] private Animator doorAnimator;

    /// <summary>
    /// Collider that blocks the door.
    /// </summary>
    [SerializeField] private Collider doorCollider;

    /// <summary>
    /// Key used to open the door.
    /// </summary>
    [SerializeField] private KeyCode openKey = KeyCode.E;

    /// <summary>
    /// Checks if the player is near the door.
    /// </summary>
    private bool playerNear;

    /// <summary>
    /// Checks if the door is currently open.
    /// </summary>
    private bool doorOpen;

    /// <summary>
    /// Checks for E input near the door.
    /// </summary>
    private void Update()
    {
        if (playerNear && Input.GetKeyDown(openKey))
        {
            TryOpenDoor();
        }
    }

    /// <summary>
    /// Detects when player enters the door trigger.
    /// </summary>
    /// <param name="other">Object entering the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            playerNear = true;

            if (GameManager.instance != null)
            {
                GameManager.instance.ShowMessage("Press E to open door");
            }
        }
    }

    /// <summary>
    /// Detects when player exits the door trigger.
    /// </summary>
    /// <param name="other">Object exiting the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            playerNear = false;

            if (doorOpen)
            {
                CloseDoor();
            }

            if (GameManager.instance != null)
            {
                GameManager.instance.ClearMessage();
            }
        }
    }

    /// <summary>
    /// Checks if the collider belongs to the player.
    /// </summary>
    /// <param name="other">Collider being checked.</param>
    /// <returns>True if the collider is the player.</returns>
    private bool IsPlayer(Collider other)
    {
        return other.CompareTag("Player") || other.GetComponentInParent<PlayerHealth>() != null;
    }

    /// <summary>
    /// Opens the door if Stage 3 coins are collected.
    /// </summary>
    private void TryOpenDoor()
    {
        if (GameManager.instance == null)
        {
            return;
        }

        if (GameManager.instance.CanOpenStage3Door())
        {
            doorOpen = true;

            if (doorAnimator != null)
            {
                doorAnimator.ResetTrigger("CloseDoor");
                doorAnimator.SetTrigger("OpenDoor");
            }

            if (doorCollider != null)
            {
                doorCollider.enabled = false;
            }

            GameManager.instance.ShowMessage("Door opened!");
        }
        else
        {
            GameManager.instance.ShowMessage("Door locked. Collect all 4 Stage 3 coins.");
        }
    }

    /// <summary>
    /// Closes the door.
    /// </summary>
    private void CloseDoor()
    {
        doorOpen = false;

        if (doorAnimator != null)
        {
            doorAnimator.ResetTrigger("OpenDoor");
            doorAnimator.SetTrigger("CloseDoor");
        }

        if (doorCollider != null)
        {
            doorCollider.enabled = true;
        }
    }
}