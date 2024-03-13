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
            Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

            if (Vector3.Angle(transform.position, directionToTarget) < coneAngle / 2)
            {
                // Raycast to check for obstacles between NPC and player
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToTarget, out hit, 500, obstacleLayer))
                {
                    // Debug ray for obstacle raycast
                    Debug.DrawRay(transform.position, directionToTarget * hit.distance, Color.red);

                    // Obstacle detected, player not visible
                    if (hit.collider.CompareTag("Obstacle"))
                    {
                        return TaskStatus.FAILURE;
                    }
                    else
                    {
                        // Debug ray for no obstacle raycast
                        Debug.DrawRay(transform.position, directionToTarget * 500, Color.green);
                    }

                    // No obstacles in the way, player visible
                    Guard.canSeePlayer = true;
                    return TaskStatus.SUCCESS;
                }
            }
        }
        // Player not within cone or not found
        return TaskStatus.FAILURE;
    }
}