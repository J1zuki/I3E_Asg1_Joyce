/*
* Description: Moves an obstacle left and right.
*/

using UnityEngine;

/// Moves an obstacle back and forth.
public class MovingObstacle : MonoBehaviour
{
    /// How far the obstacle moves.
    [SerializeField] private Vector3 moveDistance = new Vector3(4f, 0f, 0f);

    /// Speed of movement.
    [SerializeField] private float speed = 2f;

    /// Starting position.
    private Vector3 startPos;

    /// Ending position.
    private Vector3 endPos;

    /// Current move direction.
    private int moveDirection = 1;

    /// Sets start and end position.
    private void Start()
    {
        startPos = transform.position;
        endPos = startPos + moveDistance;
    }

    /// Moves the obstacle.
    private void Update()
    {
        Vector3 objPosition = transform.position;
        objPosition += moveDistance.normalized * speed * Time.deltaTime * moveDirection;
        transform.position = objPosition;

        if (moveDirection == 1 && Vector3.Distance(transform.position, startPos) >= moveDistance.magnitude)
        {
            transform.position = endPos;
            moveDirection = -1;
        }

        if (moveDirection == -1 && Vector3.Distance(transform.position, endPos) >= moveDistance.magnitude)
        {
            transform.position = startPos;
            moveDirection = 1;
        }
    }
}