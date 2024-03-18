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
            Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < coneAngle / 2)
            {
                // Draw the debug ray
                Debug.DrawRay(transform.position, directionToTarget, Color.green);
                float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                {
                    return TaskStatus.FAILURE;
                }
                return TaskStatus.SUCCESS;
            }
        }
        // Player not within cone or not found
        return TaskStatus.FAILURE;
    }
}
