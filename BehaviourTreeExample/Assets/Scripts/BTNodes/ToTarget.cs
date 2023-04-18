using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ToTarget : Node
{
    private Transform transform;
    private Animator animator;
    private Vector3 lastKnownPosition;
    private float cooldown = 2f;
    private NavMeshAgent agent;
    public ToTarget(Transform _transform) 
    {
        transform = _transform;
        animator = transform.GetComponentInChildren<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
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
                if (Vector3.Distance(transform.position, target.position) > 0.01f)
                {
                    animator.Play("Rifle Walk");
                    lastKnownPosition = target.position;
                    agent.SetDestination(target.position);
                    transform.LookAt(target.position);
                }
            }
            else if(lastKnownPosition != Vector3.zero)
            {
                agent.SetDestination(lastKnownPosition);
                transform.LookAt(lastKnownPosition);
                if (Vector3.Distance(transform.position, lastKnownPosition) < 0.03f) 
                {
                    ClearData("Target");
                    cooldown -= Time.deltaTime;
                    animator.Play("Idle");
                    if (cooldown <= 0)
                    {
                        lastKnownPosition = Vector3.zero;
                        cooldown = 2f;
                        status = TaskStatus.Failed;
                        return status;
                    }
                }
            }
        }
        status = TaskStatus.Running;
        return status;
    }

}
