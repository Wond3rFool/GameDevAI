using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : TreeNode
{
    public Sequence(List<TreeNode> children) : base(children) { }

    public override TaskStatus Evaluate()
    {
        // Keep track of the index of the child node being evaluated
        int currentChildIndex = 0;

        // Keep evaluating child nodes until all have succeeded or one fails
        while (currentChildIndex < children.Count)
        {
            TaskStatus childStatus = children[currentChildIndex].Evaluate();

            // If the child succeeded, move on to the next child
            if (childStatus == TaskStatus.Success)
            {
                currentChildIndex++;
            }
            // If the child is still running, continue evaluating it
            else if (childStatus == TaskStatus.Running)
            {
                return TaskStatus.Running;
            }
            // If the child fails, return failure
            else
            {
                return TaskStatus.Failed;
            }
        }

        // If all child nodes succeeded, return success
        return TaskStatus.Success;
    }
}