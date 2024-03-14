using UnityEngine;
using UnityEngine.AI;

public class LookRandomly : BTBaseNode
{
    private Transform transform;
    private NavMeshAgent agent;

    private float elapsedTime;
    private float timeToWait;
    private float maxAngle = 45f;

    private bool isLooking;
    public LookRandomly(Transform transform, float timeToWait)
    {
        this.transform = transform;
        this.timeToWait = timeToWait;
        agent = transform.GetComponent<NavMeshAgent>();
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        if (!isLooking)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeToWait)
            {
                LookRandomDirection();
                isLooking = true;
                elapsedTime = 0f;
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeToWait)
            {
                elapsedTime = 0f;
                isLooking = false;
                return TaskStatus.SUCCESS;
            }
        }

        return TaskStatus.RUNNING;
    }

    public void LookRandomDirection()
    {
        float randomAngle = Random.Range(-maxAngle, maxAngle);
        
        Quaternion randomRotation = Quaternion.Euler(0f, randomAngle, 0f);

        Quaternion newRotation = agent.transform.rotation * randomRotation;

        Vector3 randomDirection = newRotation * Vector3.forward;
        Vector3 newPosition = agent.transform.position + randomDirection * 0.1f; // Adjust the magnitude as needed

        NavMeshHit hit;
        if (NavMesh.SamplePosition(newPosition, out hit, 1f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            agent.transform.rotation = newRotation;
        }
        else
        {
            // If the new position is not on the NavMesh, retry with a new random direction
            LookRandomDirection();
        }
    }
}
