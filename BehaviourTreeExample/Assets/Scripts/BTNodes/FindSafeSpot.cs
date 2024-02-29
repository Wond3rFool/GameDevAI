using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindSafeSpot : BTBaseNode
{
    private Transform transform;
    private Transform[] spots;

    private NavMeshAgent agent;
    private Animator animator;

    public FindSafeSpot(Transform transform, Transform[] spots)
    {
        this.transform = transform;
        this.spots = spots;

        agent = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponentInChildren<Animator>();
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        Transform closestSpot = FindClosestSpot();

        if (closestSpot != null)
        {
            // Assuming your agent has a NavMeshAgent component
            if (agent != null)
            {
                // Set agent's destination to the closest spot
                agent.SetDestination(closestSpot.position);
                agent.speed = 10.0f;
                animator.Play("Walk Crouch");
                if (Vector3.Distance(agent.transform.position, closestSpot.position) < 0.5f) 
                {
                    agent.SetDestination(transform.position);
                    return TaskStatus.SUCCESS;
                }
                return TaskStatus.RUNNING;
            }
        }

        return TaskStatus.FAILURE;
    }

    private Transform FindClosestSpot()
    {
        Transform closestSpot = null;
        float closestDistance = float.MaxValue;

        foreach (Transform spot in spots)
        {
            float distance = Vector3.Distance(transform.position, spot.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSpot = spot;
            }
        }

        return closestSpot;
    }
}
