using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttackRange : BTBaseNode
{
    private static int playerLayerMask = 1 << 6;

    private Transform transform;
    private Animator animator;

    public CheckAttackRange(Transform _transform) 
    {
        transform = _transform;
        animator = transform.GetComponentInChildren<Animator>();
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        object t = blackboard.GetData<object>("Target");
        if (t == null) 
        {
            return TaskStatus.FAILURE;
        }
        Transform target = (Transform)t;
        if (Vector3.Distance(transform.position, target.position) <= Guard.attackRange) 
        {
            animator.Play("Kick");
            return TaskStatus.FAILURE;
        }

        
        return TaskStatus.FAILURE;
    }
}
