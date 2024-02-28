using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConditionNode : BTBaseNode
{
    private Func<bool> condition;

    public ConditionNode(Func<bool> condition)
    {
        this.condition = condition;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        if (condition())
        {
            state = TaskStatus.SUCCESS;
        }
        else
        {
            state = TaskStatus.FAILURE;
        }
        return state;
    }
}
