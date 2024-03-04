using System.Collections;
using UnityEngine;

public class CheckForPlayer : BTBaseNode
{
    private LayerMask obstacleLayer;
    private Transform transform;
    private float coneAngle = 45f; // Set your desired cone angle here

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
            Vector3 directionToPlayer = targetTransform.position - transform.position;

            // Check if the player is within the cone angle
            if (Vector3.Angle(transform.forward, directionToPlayer) <= coneAngle * 0.5f)
            {
                // Raycast to check for obstacles between NPC and player
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, Mathf.Infinity, obstacleLayer))
                {
                    // Obstacle detected, player not visible
                    if (hit.collider.CompareTag("Obstacle"))
                    {
                        return TaskStatus.FAILURE;
                    }
                }

                // No obstacles in the way, player visible
                return TaskStatus.SUCCESS;
            }
        }

        // Player not within cone or not found
        return TaskStatus.FAILURE;
    }
}