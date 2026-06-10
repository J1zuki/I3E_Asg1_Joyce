/*
* Description: Damages player over time when inside hazard.
*/

using UnityEngine;

/// Damages the player while touching the hazard.
public class DamageHazard : MonoBehaviour
{
    /// Damage done every second.
    [SerializeField] private float damagePerSecond = 50f;

    /// Damages the player while inside the trigger.
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