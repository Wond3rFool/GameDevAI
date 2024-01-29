using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttackRange : TreeNode
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
            return TaskStatus.Failed;
        }
        Transform target = (Transform)t;
        if (Vector3.Distance(transform.position, target.position) <= Guard.attackRange) 
        {
            animator.Play("Kick");
            return TaskStatus.Success;
        }

        
        return TaskStatus.Failed;
    }
}
