using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallel : BTBaseNode
{
    public Parallel() : base(){}
    public Parallel(List<BTBaseNode> children) : base(children) { }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        bool anyChildRunning = false;
        foreach (BTBaseNode child in children) 
        {
            switch (child.Evaluate(blackboard)) 
            {
                case TaskStatus.FAILURE:
                    state = TaskStatus.FAILURE;
                    return state;
                case TaskStatus.SUCCESS:
                    continue;
                case TaskStatus.RUNNING:
                    anyChildRunning = true;
                    continue;
                default:
                    state = TaskStatus.SUCCESS;
                    return state;
            }
        }

        state = anyChildRunning ? TaskStatus.RUNNING : TaskStatus.SUCCESS;
        return state;
    }
}