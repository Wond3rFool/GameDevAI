using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttackRange : Node
{
    private static int playerLayerMask = 1 << 6;

    private Transform transform;
    private Animator animator;

    public CheckAttackRange(Transform _transform) 
    {
        transform = _transform;
        animator = transform.GetComponentInChildren<Animator>();
    }

    public override TaskStatus Evaluate()
    {
        object t = GetData("Target");
        if (t == null) 
        {
            status = TaskStatus.Failed;
            return status;
            
        }
        Transform target = (Transform)t;
        if (Vector3.Distance(transform.position, target.position) <= Guard.attackRange) 
        {
            animator.Play("Kick");
            status = TaskStatus.Success;
            return status;
        }

        status = TaskStatus.Failed;
        return status;
    }
}
