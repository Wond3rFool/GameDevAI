using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : BTBaseNode
{
    private Transform transform;
    private Animator animator;
    private NavMeshAgent agent;
    private float followDistance;
    private string target;


    public FollowPlayer(Transform _transform, float followDistance, string target) 
    {
        transform = _transform;
        this.followDistance = followDistance;
        animator = transform.GetComponentInChildren<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
        this.target = target;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        Transform playerTransform = blackboard.GetData<Transform>(target);
        Vector3 playerPosition = playerTransform.position;

        // Calculate the distance between NPC and player
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

        if (distanceToPlayer > followDistance)
        {
            agent.SetDestination(playerPosition);
            agent.speed = 2f;
            animator.Play("Walk Crouch");
            return TaskStatus.RUNNING;
        }
        else
        {
            agent.SetDestination(transform.position);
            animator.Play("Crouch Idle");
            return TaskStatus.SUCCESS;
        }
    }
}
