using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayAnimation : BTBaseNode
{
    private NavMeshAgent agent;
    private Animator animator;
    private string animationName;

    public PlayAnimation(Transform transform, string animationName) 
    {
        agent = transform.GetComponent<NavMeshAgent>();
        animator = transform.GetComponentInChildren<Animator>();
        this.animationName = animationName;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        agent.SetDestination(agent.transform.position);
        agent.isStopped = true;
        animator.Play(animationName);
        bool isPlaying = animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;

        if (isPlaying)
        {
            return TaskStatus.RUNNING;
        }
        agent.isStopped = false;
        return TaskStatus.SUCCESS;
    }

}
