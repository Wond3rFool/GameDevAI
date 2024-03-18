using UnityEngine;

public class StunTarget : BTBaseNode
{
    private Transform transform;
    private LayerMask layer;
    private string target;

    public StunTarget(Transform transform, LayerMask layer, string target) 
    {
        this.transform = transform;
        this.layer = layer;
        this.target = target;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        Transform targetToStun = blackboard.GetData<Transform>(target);
        targetToStun.GetComponent<IDamageable>().BeStunned();
        return TaskStatus.SUCCESS;
    }
}
