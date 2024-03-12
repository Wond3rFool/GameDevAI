using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckAttackRange : BTBaseNode
{
    private Transform transform;
    private Animator animator;
    private NavMeshAgent agent;

    public CheckAttackRange(Transform _transform) 
    {
        transform = _transform;
        animator = transform.GetComponentInChildren<Animator>();
        agent = _transform.GetComponent<NavMeshAgent>();
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        object t = blackboard.GetData<object>("Target");
        Transform target = (Transform)t;

        if (Vector3.Distance(transform.position, target.position) <= Guard.attackRange) 
        {
            animator.Play("Kick");
            agent.SetDestination(transform.position);
            return TaskStatus.SUCCESS;
        }

        return TaskStatus.RUNNING;
    }
}
