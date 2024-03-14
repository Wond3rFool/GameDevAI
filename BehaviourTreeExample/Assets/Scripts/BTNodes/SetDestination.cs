using UnityEngine;
using UnityEngine.AI;

public class SetDestination : BTBaseNode
{
    private Transform transform;
    private Vector3 destination;
    private string target;

    private NavMeshAgent agent;
    private Animator animator;
    public SetDestination(Transform transform, string target) 
    {
        this.transform = transform;
        this.target = target;

        agent = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponentInChildren<Animator>();
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        destination = blackboard.GetData<Vector3>(target);
        agent.isStopped = false;
        agent.SetDestination(destination);
        animator.Play("Rifle Walk");
        if (!agent.pathPending && agent.remainingDistance < 1.5f) 
        {
            return TaskStatus.SUCCESS;
        }
        return TaskStatus.RUNNING;
    }
}
