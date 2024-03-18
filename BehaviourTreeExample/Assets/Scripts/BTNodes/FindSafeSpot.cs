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
        Transform closestSpot = FindClosestReachableSpot();

        if (closestSpot != null)
        {
            // Set agent's destination to the closest reachable spot
            agent.SetDestination(closestSpot.position);
            agent.speed = 4.5f;
            animator.Play("Run");
            if (!agent.pathPending && agent.remainingDistance < 1.5f)
            {
                agent.SetDestination(transform.position);
                return TaskStatus.SUCCESS;
            }
            return TaskStatus.RUNNING;
        }
        return TaskStatus.FAILURE;
    }

    private Transform FindClosestReachableSpot()
    {
        Transform closestSpot = null;
        float closestDistance = float.MaxValue;

        foreach (Transform spot in spots)
        {
            float distance = Vector3.Distance(transform.position, spot.position);

            if (distance < closestDistance)
            {
                NavMeshPath path = new NavMeshPath();
                if (agent != null && agent.isOnNavMesh)
                {
                    if (agent.CalculatePath(spot.position, path))
                    {
                        if (path.status == NavMeshPathStatus.PathComplete)
                        {
                            closestDistance = distance;
                            closestSpot = spot;
                        }
                    }
                }
            }
        }

        return closestSpot;
    }
}
