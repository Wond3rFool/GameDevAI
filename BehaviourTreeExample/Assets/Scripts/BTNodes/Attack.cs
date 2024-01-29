using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Attack : TreeNode
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

    public override TaskStatus Evaluate()
    {
        Transform target = (Transform)GetData("Target");
        
        attackCounter += Time.deltaTime;
        text.text = "Attacking player";
        Debug.Log(text.text);
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
