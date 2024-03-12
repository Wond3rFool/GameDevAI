using System.Collections;
using UnityEngine;

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

            // Debug ray for direction to player
            Debug.DrawRay(transform.position, directionToPlayer, Color.blue);

            // Check if the player is within the cone angle
            if (Vector3.Angle(transform.forward, directionToPlayer) <= coneAngle * 0.5f)
            {
                // Raycast to check for obstacles between NPC and player
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, 500, obstacleLayer))
                {
                    // Debug ray for obstacle raycast
                    Debug.DrawRay(transform.position, directionToPlayer * hit.distance, Color.red);

                    // Obstacle detected, player not visible
                    if (hit.collider.CompareTag("Obstacle"))
                    {
                        Debug.Log("Obstacle hit: " + hit.collider.gameObject.name);
                        return TaskStatus.FAILURE;
                    }
                    else
                    {
                        // Debug ray for no obstacle raycast
                        Debug.DrawRay(transform.position, directionToPlayer * 500, Color.green);
                        Guard.canSeePlayer = true;
                        return TaskStatus.SUCCESS;
                    }
                }
            }
            // Player not within cone or not found
            return TaskStatus.FAILURE;
        }
        // Player not within cone or not found
        return TaskStatus.FAILURE;
    }
}