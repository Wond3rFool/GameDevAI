using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunTarget : BTBaseNode
{
    public StunTarget(Transform transform, LayerMask layer) 
    {
        //save for later if maybe I want to actually use a projectile.
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        Guard.isStunned = true;
        return TaskStatus.SUCCESS;
    }
}
