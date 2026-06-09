/*
* Author: Your Name
* Date: 2026
* Description: Moves an obstacle left and right.
*/

using UnityEngine;

/// <summary>
/// Moves an obstacle back and forth.
/// </summary>
public class MovingObstacle : MonoBehaviour
{
    /// <summary>
    /// How far the obstacle moves.
    /// </summary>
    [SerializeField] private Vector3 moveDistance = new Vector3(4f, 0f, 0f);

    /// <summary>
    /// Speed of movement.
    /// </summary>
    [SerializeField] private float speed = 2f;

    /// <summary>
    /// Starting position.
    /// </summary>
    private Vector3 startPos;

    /// <summary>
    /// Ending position.
    /// </summary>
    private Vector3 endPos;

    /// <summary>
    /// Sets start and end position.
    /// </summary>
    private void Start()
    {
        startPos = transform.position;
        endPos = startPos + moveDistance;
    }

    /// <summary>
    /// Moves the obstacle.
    /// </summary>
    private void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPos, endPos, t);
    }
}