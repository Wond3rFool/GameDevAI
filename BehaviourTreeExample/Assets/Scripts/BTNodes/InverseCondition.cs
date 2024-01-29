using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InverseCondition : TreeNode
{
    private Func<bool> condition;

    public InverseCondition(Func<bool> condition)
    {
        this.condition = condition;
    }

    public override TaskStatus Evaluate()
    {
        if (condition())
        {
            status = TaskStatus.Failed;
        }
        else
        {
            status = TaskStatus.Success;
        }
        return status;
    }
}