using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckForTarget : BTBaseNode
{
    private LayerMask obstacleLayer;
    private Transform transform;
    private string target;
    private float coneAngle; // Set your desired cone angle here

    public CheckForTarget(Transform transform, LayerMask obstacleLayer, string target, float coneAngle)
    {
        this.obstacleLayer = obstacleLayer;
        this.target = target;
        this.transform = transform;
        this.coneAngle = coneAngle;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        object targetToFind = blackboard.GetData<object>(target);
        Transform targetTransform = (Transform)targetToFind;

        if (targetTransform != null)
        {
            // Calculate direction to the player
            Vector3 directionToPlayer = targetTransform.position - transform.position;
            // Check if the player is within the cone angle
            if (Vector3.Angle(transform.forward, directionToPlayer) <= coneAngle * 0.5f)
            {
                
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, 500, obstacleLayer))
                {
                    // Obstacle detected, player not visible
                    if (hit.collider.CompareTag("Obstacle"))
                    {
                        return TaskStatus.FAILURE;
                    }
                }

                // No obstacles in the way, player visible
                Guard.canSeePlayer = true;
                return TaskStatus.SUCCESS;
            }
        }
        // Player not within cone or not found
        return TaskStatus.FAILURE;
    }
}
