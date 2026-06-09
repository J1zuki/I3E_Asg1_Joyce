/*
* Description: Opens the Stage 3 door only after 4 Stage 3 coins are collected.
*/

using UnityEngine;

/// <summary>
/// Controls the locked Stage 3 door.
/// </summary>
public class Stage3Door : MonoBehaviour
{
    /// <summary>
    /// The door object that rotates open.
    /// </summary>
    [SerializeField] private Transform doorObject;

    /// <summary>
    /// The collider blocking the player.
    /// </summary>
    [SerializeField] private Collider doorCollider;

    /// <summary>
    /// Key used to open the door.
    /// </summary>
    [SerializeField] private KeyCode openKey = KeyCode.E;

    /// <summary>
    /// Door open angle.
    /// </summary>
    [SerializeField] private Vector3 openAngle = new Vector3(0f, 90f, 0f);

    /// <summary>
    /// Door opening speed.
    /// </summary>
    [SerializeField] private float openSpeed = 3f;

    /// <summary>
    /// Sound played when the door opens.
    /// </summary>
    [SerializeField] private AudioClip openSound;

    /// <summary>
    /// Sound played when the door is locked.
    /// </summary>
    [SerializeField] private AudioClip lockedSound;

    /// <summary>
    /// Checks if player is near the door.
    /// </summary>
    private bool playerNear;

    /// <summary>
    /// Checks if the door is already open.
    /// </summary>
    private bool opened;

    /// <summary>
    /// Door closed rotation.
    /// </summary>
    private Quaternion closedRotation;

    /// <summary>
    /// Door opened rotation.
    /// </summary>
    private Quaternion openedRotation;

    /// <summary>
    /// Sets the door rotations.
    /// </summary>
    private void Start()
    {
        closedRotation = doorObject.rotation;
        openedRotation = closedRotation * Quaternion.Euler(openAngle);
    }

    /// <summary>
    /// Checks input and opens door if allowed.
    /// </summary>
    private void Update()
    {
        if (playerNear && Input.GetKeyDown(openKey))
        {
            TryOpenDoor();
        }

        if (opened)
        {
            doorObject.rotation = Quaternion.Lerp(doorObject.rotation, openedRotation, Time.deltaTime * openSpeed);
        }
    }

    /// <summary>
    /// Detects when player enters door trigger.
    /// </summary>
    /// <param name="other">Object that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;

            if (GameManager.instance != null)
            {
                GameManager.instance.ShowMessage("Press E to open door");
            }
        }
    }

    /// <summary>
    /// Detects when player exits door trigger.
    /// </summary>
    /// <param name="other">Object that exited the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            if (GameManager.instance != null)
            {
                GameManager.instance.ClearMessage();
            }
        }
    }

    /// <summary>
    /// Opens the door if 4 Stage 3 coins are collected.
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