using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class ToTarget : TreeNode
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

    public override TaskStatus Evaluate()
    {
        Transform target = (Transform)(GetData("Target"));
        Vector3 dir = target.transform.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit))
        {
            if (hit.transform.GetComponent<Player>())
            {
                if (Vector3.Distance(transform.position, target.position) > 0.4f)
                {
                    animator.Play("Rifle Walk");
                    agent.SetDestination(target.position);
                    transform.LookAt(target.position);
                    text.text = "Player in sight";
                    Debug.Log(text.text);
                    return TaskStatus.Running;
                }
                else 
                {
                    return TaskStatus.Success;
                }
            }
            else 
            {
                ClearData("Target");
                status = TaskStatus.Failed;
                return status;
            }
        }
        return TaskStatus.Failed;
    }
}



