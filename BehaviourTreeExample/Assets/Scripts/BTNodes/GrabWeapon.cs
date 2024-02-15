using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GrabWeapon : TreeNode
{
    private Transform transform;
    private Transform target;
    private Animator animator;
    private NavMeshAgent agent;
    private TextMeshPro text;

    public GrabWeapon(Transform _transform, Transform _target, TextMeshPro _text) 
    {
        transform = _transform;
        target = _target;
        text = _text;

        animator = transform.GetComponentInChildren<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
    }

    public override TaskStatus Evaluate()
    {
        if (Vector3.Distance(transform.position, target.position) < 2.5f)
        {
            transform.position = target.position;
            text.text = "Found weapon";
            Guard.hasWeapon = true;
            Debug.Log(text.text);
            return TaskStatus.Success;
        }
        else
        {
            animator.Play("Rifle Walk");
            agent.SetDestination(target.position);
            transform.LookAt(target.position);
            text.text = "Finding Weapon";
            Debug.Log(text.text);
            return TaskStatus.Running;
        }
    }
}
