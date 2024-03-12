using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckAttackRange : BTBaseNode
{
    private Transform transform;
    private Animator animator;
    private NavMeshAgent agent;
    private float attackRange;
    private string target;
    private string animationName;

    public CheckAttackRange(Transform transform, float attackRange, string target, string animationName) 
    {
        this.transform = transform;
        this.attackRange = attackRange;
        this.target = target;
        this.animationName = animationName;
        animator = transform.GetComponentInChildren<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        object targetToHit = blackboard.GetData<object>(target);
        Transform targetTransform = (Transform)targetToHit;

        if (Vector3.Distance(transform.position, targetTransform.position) <= attackRange) 
        {
            animator.Play(animationName);
            agent.SetDestination(transform.position);
            return TaskStatus.SUCCESS;
        }

        return TaskStatus.RUNNING;
    }
}
