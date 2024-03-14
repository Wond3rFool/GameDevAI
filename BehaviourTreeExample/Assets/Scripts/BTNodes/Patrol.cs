using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Patrol : BTBaseNode
{
    private Transform transform;
    private Animator animator;
    private NavMeshAgent agent;
    private Transform[] waypoints;
    private Transform currentWaypoint;
    private TextMeshPro text;
    private int currentWaypointIndex = 0;

    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool waiting = false;

    public Patrol(Transform _transform, Transform[] _waypoints, TextMeshPro _text) 
    {
        transform = _transform;
        text = _text;
        animator = transform.GetComponentInChildren<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
        waypoints = _waypoints; 
        currentWaypoint = waypoints[currentWaypointIndex];
        agent.SetDestination(currentWaypoint.position);
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            animator.Play("Idle");
            text.text = "Waiting";
            Debug.Log(agent.transform.name + ": " + text.text);
            if (waitCounter > waitTime)
            {
                waiting = false;
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                currentWaypoint = waypoints[currentWaypointIndex];
                agent.SetDestination(currentWaypoint.position);
            }
            state = TaskStatus.SUCCESS;
            return state;
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.4f)
            {
                waitCounter = 0f;
                waiting = true;
                state = TaskStatus.SUCCESS;
                return state;
            }
            else
            {
                animator.Play("Rifle Walk");
                text.text = "Patrolling";
                Debug.Log(agent.transform.name + ": " + text.text);
                state = TaskStatus.SUCCESS;
                return state;
            }
        }
    }
}
