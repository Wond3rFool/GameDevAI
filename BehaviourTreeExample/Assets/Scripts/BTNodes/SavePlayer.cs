using UnityEngine;
using UnityEngine.AI;
public class SavePlayer : BTBaseNode
{
    private Animator animator;

    public SavePlayer(Transform transform) 
    {
        animator = transform.GetComponentInChildren<Animator>();
    }
    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        animator.Play("Throw", 0, 0);
        if(!(animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime))
        { 
            return TaskStatus.RUNNING;
        }
        return TaskStatus.SUCCESS;
    }
}
