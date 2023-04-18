using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Node
{
    private Animator animator;

    private Transform lastTarget;

    private float attackTime = 1f;
    private float attackCounter = 0f;
    public Attack(Transform _transform) 
    {
        animator =  _transform.GetComponentInChildren<Animator>();
    }

    public override TaskStatus Evaluate()
    {
        Transform target = (Transform)GetData("Target");
        
        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime && target != null) 
        {
            target.GetComponent<Player>().TakeDamage(target.gameObject, 1);
            ClearData("Target");
            attackCounter = 0f;
            status = TaskStatus.Success;
            return status;
        }
        status = TaskStatus.Running;
        return status;
    }
}
