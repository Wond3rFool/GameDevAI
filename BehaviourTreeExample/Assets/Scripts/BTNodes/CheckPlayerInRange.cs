using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerInRange : BTBaseNode
{
    private static int playerLayerMask = 1 << 6;

    private Transform transform;

    public CheckPlayerInRange(Transform _transform)
    {
        transform = _transform;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        object t = blackboard.GetData<object>("Target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                transform.position, Guard.fovRange, playerLayerMask);

            if (colliders.Length > 0)
            {
                blackboard.SetData("Target", colliders[0].transform);
                Guard.hasVision = true;
                state = TaskStatus.SUCCESS;
                return state;
            }
            state = TaskStatus.FAILURE;
            return state;
        }
        state = TaskStatus.SUCCESS;
        return state;
    }

}
