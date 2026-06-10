/*
* Description: Shows the win panel when the player reaches the finish line.
*/

using UnityEngine;

/// Detects when the player reaches the finish line.
public class FinishLine : MonoBehaviour
{
    /// Checks if the finish line has already been triggered.
    private bool finished;

    /// Detects when the player enters the finish trigger.
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

            print("Player reached finish line.");
        }
    }
}