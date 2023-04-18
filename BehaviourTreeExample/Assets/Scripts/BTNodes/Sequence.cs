using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public Sequence() : base(){}
    public Sequence(List<Node> children) : base(children) { }

    public override TaskStatus Evaluate()
    {
        bool anyChildRunning = false;
        foreach (Node child in children) 
        {
            switch (child.Evaluate()) 
            {
                case TaskStatus.Failed:
                    status = TaskStatus.Failed;
                    return status;
                case TaskStatus.Success:
                    continue;
                case TaskStatus.Running:
                    anyChildRunning = true;
                    continue;
                default:
                    status = TaskStatus.Success;
                    return status;
            }
        }

        status = anyChildRunning ? TaskStatus.Running : TaskStatus.Success;
        return status;
    }
}