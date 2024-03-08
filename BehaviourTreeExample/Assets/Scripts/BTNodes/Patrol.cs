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
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            animator.Play("Idle");
            text.text = "Idling";
            agent.SetDestination(transform.position);
            Debug.Log(text.text);
            if (waitCounter > waitTime) 
            {
                waiting = false;
            }
            state = TaskStatus.SUCCESS;
            return state;
        }
        else 
        {
            Transform wp = waypoints[currentWaypointIndex];
            if (Vector3.Distance(transform.position, wp.position) < 0.4f)
            {
                transform.position = wp.position;
                waitCounter = 0f;
                waiting = true;
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                state = TaskStatus.SUCCESS;
                return state;
            }
            else 
            {
                animator.Play("Rifle Walk");
                agent.SetDestination(wp.position);
                text.text = "Patrolling";
                Debug.Log(text.text);
                state = TaskStatus.SUCCESS;
                return state;
            }
        }
    }
}
