using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionNode : BTBaseNode
{
    private readonly Action function;

    public FunctionNode(Action function)
    {
        this.function = function;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        function?.Invoke();
        return TaskStatus.SUCCESS;
    }
}