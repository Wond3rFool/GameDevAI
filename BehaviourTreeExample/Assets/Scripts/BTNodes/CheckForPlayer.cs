using System.Collections;
using UnityEngine;

public class CheckForPlayer : BTBaseNode
{
    private static int playerLayerMask = 1 << 6;
    private LayerMask obstacleLayer;
    private Transform transform;

    public CheckForPlayer(Transform _transform, LayerMask obstacleLayer)
    {
        this.obstacleLayer = obstacleLayer;
        transform = _transform;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        object t = blackboard.GetData<object>("Target");
        Transform targetTransform = (Transform)t;

        if (t != null)
        {
            // Calculate direction to the player
            Vector3 direction = targetTransform.position - transform.position;

            // Raycast to check for obstacles
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, direction, direction.magnitude, obstacleLayer);

            // Check if any obstacle is hit
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("Obstacle"))
                {
                    // Obstacle in the way
                    Guard.isStunned = false;
                    return TaskStatus.FAILURE;
                }
            }

            // No obstacles, player is in line of sight
            Guard.isStunned = true;
            return TaskStatus.SUCCESS;
        }

        return TaskStatus.FAILURE;
    }
}