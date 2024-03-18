using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTargetInRange : BTBaseNode
{
    private LayerMask layer;

    private Transform transform;

    private float range;

    private string target;
    public CheckTargetInRange(Transform transform, float range, string target, LayerMask layer)
    {
        this.transform = transform;
        this.range = range;
        this.target = target;
        this.layer = layer;
     }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        object targetObject = blackboard.GetData<object>(target);
        if (targetObject == null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                transform.position, range, layer);

            if (colliders.Length > 0)
            {
                blackboard.SetData(target, colliders[0].transform);
                state = TaskStatus.SUCCESS;
                return state;
            }
            state = TaskStatus.FAILURE;
            return state;
        }
        else 
        {
            Transform target = (Transform)targetObject;
            if (Vector3.Distance(transform.position, target.position) > range)
            {
                target.GetComponent<Player>().SetBeingAttacked(false);
                return TaskStatus.FAILURE;
            }
            else 
            {
                return TaskStatus.SUCCESS;
            }
        } 
    }

}
