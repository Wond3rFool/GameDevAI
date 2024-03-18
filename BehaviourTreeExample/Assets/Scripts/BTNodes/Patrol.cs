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
    private TextMeshPro text;

    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool waiting = false;

    public Patrol(Transform _transform, TextMeshPro _text)
    {
        transform = _transform;
        text = _text;
        animator = transform.GetComponentInChildren<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
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
        Vector3 randomPoint = RandomPointInNavMesh();

        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(randomPoint, path))
        {
            agent.SetDestination(randomPoint);

            // Draw the path for debugging
            DrawPath(path);
        }   
        
        
    }
    void DrawPath(NavMeshPath path)
    {
        if (path.corners.Length < 2)
            return;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 10f);
        }
    }
    Vector3 RandomPointInNavMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int randomTriangleIndex = Random.Range(0, navMeshData.indices.Length / 3);
        Vector3[] vertices = navMeshData.vertices;
        int[] indices = navMeshData.indices;

        Vector3 point = Vector3.zero;

        Vector3 v1 = vertices[indices[randomTriangleIndex * 3]];
        Vector3 v2 = vertices[indices[randomTriangleIndex * 3 + 1]];
        Vector3 v3 = vertices[indices[randomTriangleIndex * 3 + 2]];

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
