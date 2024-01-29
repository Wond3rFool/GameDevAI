using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerInRange : TreeNode
{
    private static int playerLayerMask = 1 << 6;

    private Transform transform;

    public CheckPlayerInRange(Transform _transform)
    {
        transform = _transform;
    }

    public override TaskStatus Evaluate()
    {
        object t = GetData("Target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                transform.position, Guard.fovRange, playerLayerMask);

            if (colliders.Length > 0)
            {
                parent.parent.SetData("Target", colliders[0].transform);
                status = TaskStatus.Success;
                return status;
            }
            status = TaskStatus.Failed;
            return status;
        }
        Transform target = (Transform)t;
        if (Vector3.Distance(transform.position, target.position) > Guard.fovRange) 
        {
            parent.parent.ClearData("Target");
            status = TaskStatus.Failed;
            return status;
        }
        status = TaskStatus.Success;
        return status;
    }

}
