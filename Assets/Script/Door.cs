/*
* Author: Your Name
* Date: 2026
* Description: Opens the Stage 3 door animation with E after 4 Stage 3 coins are collected.
*/

using UnityEngine;

/// <summary>
/// Controls the Stage 3 locked door.
/// </summary>
public class Stage3Door : MonoBehaviour
{
    /// <summary>
    /// Animator attached to the door object.
    /// </summary>
    [SerializeField] private Animator doorAnimator;

    /// <summary>
    /// Collider that blocks the player before the door opens.
    /// </summary>
    [SerializeField] private Collider doorCollider;

    /// <summary>
    /// Key used to open the door.
    /// </summary>
    [SerializeField] private KeyCode openKey = KeyCode.E;

    /// <summary>
    /// Time before the door collider turns off.
    /// </summary>
    [SerializeField] private float disableColliderDelay = 1f;

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
        if (playerNear && Input.GetKeyDown(openKey) && !opened)
        {
            TryOpenDoor();
        }
    }

    /// <summary>
    /// Detects when the player enters the door trigger.
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
    /// Detects when the player exits the door trigger.
    /// </summary>
    /// <param name="other">Object exiting the trigger.</param>
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
    /// Checks if the collider belongs to the player.
    /// </summary>
    /// <param name="other">Collider to check.</param>
    /// <returns>True if the collider belongs to the player.</returns>
    private bool IsPlayer(Collider other)
    {
        return other.CompareTag("Player") || other.GetComponentInParent<PlayerHealth>() != null;
    }

    /// <summary>
    /// Opens the door if all Stage 3 coins have been collected.
    /// </summary>
    private void TryOpenDoor()
    {
        if (GameManager.instance == null)
        {
            return;
        }

        if (GameManager.instance.CanOpenStage3Door())
        {
            opened = true;

            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger("OpenDoor");
            }

            Invoke(nameof(TurnOffDoorCollider), disableColliderDelay);

            GameManager.instance.ShowMessage("Door opened!");
        }
        else
        {
            GameManager.instance.ShowMessage("Door locked. Collect all 4 Stage 3 coins.");
        }
    }

    /// <summary>
    /// Turns off the door collider after the animation starts.
    /// </summary>
    private void TurnOffDoorCollider()
    {
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }
    }
}