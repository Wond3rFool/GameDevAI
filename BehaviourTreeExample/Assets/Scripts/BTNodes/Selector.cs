using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public Selector() : base() { }
    public Selector(List<Node> children) : base(children) { }

    public override TaskStatus Evaluate()
    {
        foreach (Node child in children)
        {
            switch (child.Evaluate())
            {
                case TaskStatus.Failed:
                    continue;
                case TaskStatus.Success:
                    status = TaskStatus.Success;
                    return status;
                case TaskStatus.Running:
                    status = TaskStatus.Running;
                    return status;
                default:
                    continue;
            }
        }

        status = TaskStatus.Failed;
        return status;
    }
}
