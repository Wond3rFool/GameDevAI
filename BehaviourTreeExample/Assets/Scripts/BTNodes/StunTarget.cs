using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunTarget : BTBaseNode
{
    public StunTarget(Transform transform, LayerMask layer) 
    {
                   
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        Guard.isStunned = true;
        return TaskStatus.SUCCESS;
    }
}
