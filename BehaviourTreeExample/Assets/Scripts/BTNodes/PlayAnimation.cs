using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayAnimation : BTBaseNode
{
    private NavMeshAgent agent;
    private Animator animator;
    private string animationName;

    private bool animationStarted = false;

    public PlayAnimation(Transform transform, string animationName) 
    {
        agent = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponentInChildren<Animator>();
        this.animationName = animationName;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        agent.SetDestination(agent.transform.position);

        if (!animationStarted)
        {
            animator.Play(animationName);
            animationStarted = true;
            return TaskStatus.RUNNING;
        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                animationStarted = false;
                return TaskStatus.SUCCESS;
            }
        }
        return TaskStatus.RUNNING;
    }

}
