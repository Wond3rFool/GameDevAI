using UnityEngine;
using TMPro;

public class Attack : BTBaseNode
{
    private TextMeshPro text;
    private Transform transform;
    private string target;
    private float attackTime = 1f;
    private float attackCounter = 0f;
    public Attack(Transform transform, TextMeshPro text, string target) 
    {
        this.text = text;
        this.transform = transform;
        this.target = target;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        Transform targetToAttack = blackboard.GetData<Transform>(target);
        
        attackCounter += Time.deltaTime;
        text.text = "Attacking " + targetToAttack.name;
        Debug.Log(text.text);
        if (attackCounter >= attackTime && targetToAttack != null) 
        {
            targetToAttack.GetComponent<IDamageable>().TakeDamage(transform.gameObject, 1);
            blackboard.RemoveData("Target");
            attackCounter = 0f;
            state = TaskStatus.SUCCESS;
            return state;
        }
        state = TaskStatus.RUNNING;
        return state;
    }
}
