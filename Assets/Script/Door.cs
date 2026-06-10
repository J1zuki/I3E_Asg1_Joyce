/*
* Description: Opens the Stage 3 door with E after 4 coins, and closes it when player walks away.
*/

using UnityEngine;

/// Controls the Stage 3 door using animation triggers.
public class Stage3Door : MonoBehaviour
{
    /// Animator attached to the door object.
    [SerializeField] private Animator doorAnimator;

    /// Collider that blocks the door.
    [SerializeField] private Collider doorCollider;

    /// Key used to open the door.
    [SerializeField] private KeyCode openKey = KeyCode.E;

    /// Checks if the player is near the door.
    private bool playerNear;

    /// Checks if the door is currently open.
    private bool doorOpen;

    /// Gets needed components when the game starts.
    private void Start()
    {
        if (doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
        }

        if (doorCollider == null)
        {
            doorCollider = GetComponent<Collider>();
        }
    }

    /// Checks for E input near the door.
    private void Update()
    {
        if (playerNear && Input.GetKeyDown(openKey))
        {
            TryOpenDoor();
        }
    }

    /// Detects when player enters the door trigger.
    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            playerNear = true;

            if (GameManager.instance != null)
            {
                if (doorOpen)
                {
                    GameManager.instance.ShowMessage("Door is open");
                }
                else
                {
                    GameManager.instance.ShowMessage("Press E to open door");
                }
            }
        }
    }

    /// Detects when player exits the door trigger.
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

    /// Checks if the collider belongs to the player.
    private bool IsPlayer(Collider other)
    {
        return other.CompareTag("Player") || other.GetComponentInParent<PlayerHealth>() != null;
    }

    /// Opens the door if Stage 3 coins are collected.
    private void TryOpenDoor()
    {
        if (doorOpen)
        {
            return;
        }

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

    /// Closes the door.
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