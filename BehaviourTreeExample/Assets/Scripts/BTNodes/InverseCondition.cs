using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InverseCondition : BTBaseNode
{
    private Func<bool> condition;

    public InverseCondition(Func<bool> condition)
    {
        this.condition = condition;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        if (condition())
        {
            state = TaskStatus.FAILURE;
        }
        else
        {
            state = TaskStatus.SUCCESS;
        }
        return state;
    }
}