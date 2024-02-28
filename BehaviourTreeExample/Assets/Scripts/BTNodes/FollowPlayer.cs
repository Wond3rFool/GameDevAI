using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : BTBaseNode
{
    private Transform transform;
    private Animator animator;
    private NavMeshAgent agent;

    public static bool playerSpotted;

    public FollowPlayer(Transform _transform) 
    {
        transform = _transform;
        animator = transform.GetComponentInChildren<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {


        return TaskStatus.RUNNING;
    }
}
