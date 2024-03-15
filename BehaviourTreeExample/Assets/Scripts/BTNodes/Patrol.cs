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
                SetRandomDestination();
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

    void SetRandomDestination()
    {
        // Generate a random point within the NavMesh volume
        Vector3 randomPoint = RandomPointInNavMesh();

        // Check if the destination is reachable
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(randomPoint, path))
        {
            // Set the sampled point as the destination for the NavMesh agent
            agent.SetDestination(randomPoint);
        }
        else
        {
            // If destination is not reachable, try again
            SetRandomDestination();
        }
    }

    Vector3 RandomPointInNavMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // Pick a random triangle from the nav mesh
        int randomTriangleIndex = Random.Range(0, navMeshData.indices.Length / 3);
        Vector3[] vertices = navMeshData.vertices;
        int[] indices = navMeshData.indices;

        Vector3 point = Vector3.zero;

        // Get vertices of the selected triangle
        Vector3 v1 = vertices[indices[randomTriangleIndex * 3]];
        Vector3 v2 = vertices[indices[randomTriangleIndex * 3 + 1]];
        Vector3 v3 = vertices[indices[randomTriangleIndex * 3 + 2]];

        // Generate a random point within the triangle
        float r1 = Random.value;
        float r2 = Random.value;
        if (r1 + r2 < 1)
        {
            point = v1 + r1 * (v2 - v1) + r2 * (v3 - v1);
        }
        else
        {
            point = v3 + (1 - r1) * (v2 - v3) + (1 - r2) * (v1 - v3);
        }

        return point;
    }
}
