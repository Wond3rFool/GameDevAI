using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionNode : TreeNode
{
    private readonly Action function;

    public FunctionNode(Action function)
    {
        this.function = function;
    }

    public override TaskStatus Evaluate()
    {
        function?.Invoke();
        return TaskStatus.Success;
    }
}