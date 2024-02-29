using UnityEngine;
using UnityEngine.AI;
public class SavePlayer : BTBaseNode
{
    private Animator animator;
    private NavMeshAgent agent;

    private Transform transform;
    private LayerMask target;

    public SavePlayer(Transform transform, LayerMask target) 
    {
        this.transform = transform;
        this.target = target;

        agent = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponentInChildren<Animator>();
    }
    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        animator.Play("Throw");
        return TaskStatus.RUNNING;
    }
}
