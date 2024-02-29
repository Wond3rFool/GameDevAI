using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class ToTarget : BTBaseNode
{
    private Transform transform;
    private Animator animator;
    private NavMeshAgent agent;
    private TextMeshPro text;
    public ToTarget(Transform _transform, TextMeshPro _text)
    {
        transform = _transform;
        animator = transform.GetComponentInChildren<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
        text = _text;

    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        Transform target = blackboard.GetData<Transform>("Target");

        if (Vector3.Distance(transform.position, target.position) > 2.0f)
        {
            animator.Play("Rifle Walk");
            agent.SetDestination(target.position);
            transform.LookAt(target.position);
            text.text = "Player in sight";
            Player.beingAttacked = true;
            Debug.Log(text.text);
            return TaskStatus.RUNNING;
        }
        else
        {
            return TaskStatus.SUCCESS;
        }


    }
}



