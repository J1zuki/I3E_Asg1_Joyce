/*
* Description: Damages player over time when inside hazard.
*/

using UnityEngine;

/// <summary>
/// Damages the player while touching the hazard.
/// </summary>
public class DamageHazard : MonoBehaviour
{
    /// <summary>
    /// Damage done every second.
    /// </summary>
    [SerializeField] private float damagePerSecond = 20f;

    /// <summary>
    /// Damages player while inside the trigger.
    /// </summary>
    /// <param name="other">Object inside the trigger.</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}