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
    [SerializeField] private float damagePerSecond = 50f;

    /// <summary>
    /// Damages player while inside the trigger.
    /// </summary>
    /// <param name="other">Object inside the trigger.</param>
    private void OnTriggerStay(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            playerHealth = other.GetComponentInParent<PlayerHealth>();
        }

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}