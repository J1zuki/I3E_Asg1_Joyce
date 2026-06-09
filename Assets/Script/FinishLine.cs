/*
* Author: Your Name
* Date: 2026
* Description: Shows the win panel when the player reaches the finish line.
*/

using UnityEngine;

/// <summary>
/// Detects when the player reaches the finish line.
/// </summary>
public class FinishLine : MonoBehaviour
{
    /// <summary>
    /// Checks if the finish line has already been triggered.
    /// </summary>
    private bool finished;

    /// <summary>
    /// Detects when the player enters the finish trigger.
    /// </summary>
    /// <param name="other">Object that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (finished)
        {
            return;
        }

        if (other.CompareTag("Player") || other.GetComponentInParent<PlayerHealth>() != null)
        {
            finished = true;

            if (GameManager.instance != null)
            {
                GameManager.instance.WinGame();
            }

            Debug.Log("Player reached finish line.");
        }
    }
}