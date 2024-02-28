using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Attack : BTBaseNode
{
    private Animator animator;

    private Transform lastTarget;
    private TextMeshPro text;

    private float attackTime = 1f;
    private float attackCounter = 0f;
    public Attack(Transform _transform, TextMeshPro _text) 
    {
        animator =  _transform.GetComponentInChildren<Animator>();
        text = _text;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        Transform target = blackboard.GetData<Transform>("Target");
        
        attackCounter += Time.deltaTime;
        text.text = "Attacking player";
        Debug.Log(text.text);
        if (attackCounter >= attackTime && target != null) 
        {
            target.GetComponent<Player>().TakeDamage(target.gameObject, 1);
            blackboard.RemoveData("Target");
            attackCounter = 0f;
            state = TaskStatus.SUCCESS;
            return state;
        }
        state = TaskStatus.RUNNING;
        return state;
    }
}
