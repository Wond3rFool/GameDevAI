using System.Collections.Generic;

public class Selector : BTBaseNode
{
    public Selector() : base() { }
    public Selector(List<BTBaseNode> children) : base(children) { }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        foreach (BTBaseNode child in children)
        {
            switch (child.Evaluate(blackboard))
            {
                case TaskStatus.FAILURE:
                    state = TaskStatus.FAILURE;
                    continue;
                case TaskStatus.SUCCESS:
                    state = TaskStatus.SUCCESS;
                    return state;
                case TaskStatus.RUNNING:
                    break;
            }
        }

        state = TaskStatus.FAILURE;
        return state;
    }
}
