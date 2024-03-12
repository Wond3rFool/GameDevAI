using UnityEngine;

public class StunTarget : BTBaseNode
{
    private Transform transform;
    private LayerMask layer;

    public StunTarget(Transform transform, LayerMask layer) 
    {
        this.transform = transform;
        this.layer = layer;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        Guard.isStunned = true;
        return TaskStatus.SUCCESS;
    }
}
