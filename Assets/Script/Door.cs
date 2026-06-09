/*
* Author: Your Name
* Date: 2026
* Description: Opens the Stage 3 door only after 4 Stage 3 coins are collected.
*/

using UnityEngine;

/// <summary>
/// Controls the locked Stage 3 door.
/// </summary>
public class Stage3Door : MonoBehaviour
{
    /// <summary>
    /// The actual door object that will move open.
    /// </summary>
    [SerializeField] private Transform doorObject;

    /// <summary>
    /// The collider that blocks the player.
    /// </summary>
    [SerializeField] private Collider doorCollider;

    /// <summary>
    /// Key used to open the door.
    /// </summary>
    [SerializeField] private KeyCode openKey = KeyCode.E;

    /// <summary>
    /// How far the door moves when it opens.
    /// </summary>
    [SerializeField] private Vector3 openOffset = new Vector3(0f, 4f, 0f);

    /// <summary>
    /// Speed of the door opening.
    /// </summary>
    [SerializeField] private float openSpeed = 3f;

    /// <summary>
    /// Sound played when the door opens.
    /// </summary>
    [SerializeField] private AudioClip openSound;

    /// <summary>
    /// Sound played when the door is still locked.
    /// </summary>
    [SerializeField] private AudioClip lockedSound;

    /// <summary>
    /// Checks if the player is near the door trigger.
    /// </summary>
    private bool playerNear;

    /// <summary>
    /// Checks if the door is opened.
    /// </summary>
    private bool opened;

    /// <summary>
    /// Door closed position.
    /// </summary>
    private Vector3 closedPosition;

    /// <summary>
    /// Door opened position.
    /// </summary>
    private Vector3 openedPosition;

    /// <summary>
    /// Sets up the door position.
    /// </summary>
    private void Start()
    {
        if (doorObject == null)
        {
            Debug.LogError("Door Object is missing. Drag the actual Door into Door Object.");
            return;
        }

        closedPosition = doorObject.position;
        openedPosition = closedPosition + openOffset;
    }

    /// <summary>
    /// Checks for input and moves the door when opened.
    /// </summary>
    private void Update()
    {
        if (playerNear && Input.GetKeyDown(openKey))
        {
            TryOpenDoor();
        }

        if (opened && doorObject != null)
        {
            doorObject.position = Vector3.Lerp(
                doorObject.position,
                openedPosition,
                Time.deltaTime * openSpeed
            );
        }
    }

    /// <summary>
    /// Detects when the player enters the door trigger.
    /// </summary>
    /// <param name="other">Object that entered the trigger.</param>
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
    /// Detects when the player leaves the door trigger.
    /// </summary>
    /// <param name="other">Object that exited the trigger.</param>
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
    /// Checks if the object belongs to the player.
    /// </summary>
    /// <param name="other">Collider to check.</param>
    /// <returns>True if the collider belongs to the player.</returns>
    private bool IsPlayer(Collider other)
    {
        return other.CompareTag("Player") || other.GetComponentInParent<PlayerHealth>() != null;
    }

    /// <summary>
    /// Opens the door if all 4 Stage 3 coins are collected.
    /// </summary>
    private void TryOpenDoor()
    {
        if (opened)
        {
            return;
        }

        if (GameManager.instance != null && GameManager.instance.CanOpenStage3Door())
        {
            opened = true;

            if (doorCollider != null)
            {
                doorCollider.enabled = false;
            }

            if (openSound != null)
            {
                AudioSource.PlayClipAtPoint(openSound, transform.position);
            }

            GameManager.instance.ShowMessage("Door opened!");
        }
        else
        {
            if (lockedSound != null)
            {
                AudioSource.PlayClipAtPoint(lockedSound, transform.position);
            }

            if (GameManager.instance != null)
            {
                GameManager.instance.ShowMessage("Door locked. Collect all 4 Stage 3 coins.");
            }
        }
    }
}