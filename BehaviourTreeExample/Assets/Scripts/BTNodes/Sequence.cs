using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : BTBaseNode
{
    public Sequence(List<BTBaseNode> children) : base(children) { }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        // Keep track of the index of the child node being evaluated
        int currentChildIndex = 0;

        // Keep evaluating child nodes until all have succeeded or one fails
        while (currentChildIndex < children.Count)
        {
            TaskStatus childStatus = children[currentChildIndex].Evaluate(blackboard);

            // If the child succeeded, move on to the next child
            if (childStatus == TaskStatus.SUCCESS)
            {
                currentChildIndex++;
            }
            // If the child is still running, continue evaluating it
            else if (childStatus == TaskStatus.RUNNING)
            {
                return TaskStatus.RUNNING;
            }
            // If the child fails, return failure
            else
            {
                return TaskStatus.FAILURE;
            }
        }

        // If all child nodes succeeded, return success
        return TaskStatus.SUCCESS;
    }
}