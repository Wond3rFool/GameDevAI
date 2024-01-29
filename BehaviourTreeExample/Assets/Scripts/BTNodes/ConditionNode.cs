using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConditionNode : TreeNode
{
    private Func<bool> condition;

    public ConditionNode(Func<bool> condition)
    {
        this.condition = condition;
    }

    public override TaskStatus Evaluate()
    {
        if (condition())
        {
            status = TaskStatus.Success;
        }
        else
        {
            status = TaskStatus.Failed;
        }
        return status;
    }
}
