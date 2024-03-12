using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GrabWeapon : BTBaseNode
{
    private Transform transform;
    private Transform target;
    private Animator animator;
    private NavMeshAgent agent;
    private TextMeshPro text;

    public GrabWeapon(Transform _transform, Transform _target) 
    {
        transform = _transform;
        target = _target;

        animator = transform.GetComponentInChildren<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        if (Vector3.Distance(transform.position, target.position) < 1.5f)
        {
            transform.position = target.position;
            agent.SetDestination(transform.position);
            return TaskStatus.SUCCESS;
        }
        else
        {
            animator.Play("Rifle Walk");
            agent.SetDestination(target.position);
            return TaskStatus.RUNNING;
        }
    }
}
