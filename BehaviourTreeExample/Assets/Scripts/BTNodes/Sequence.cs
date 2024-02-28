using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : BTBaseNode
{
	SequenceState storedState;
	public Sequence(List<BTBaseNode> children) : base(children) { }

	public override TaskStatus Evaluate(Blackboard blackboard)
	{
		// If there is no stored state, start from the beginning
		if (storedState == null)
		{
			int currentChildIndex = 0;
			storedState = new SequenceState(currentChildIndex);
		}

		// Continue evaluating child nodes from the stored state
		while (storedState.currentChildIndex < children.Count)
		{
			TaskStatus childStatus = children[storedState.currentChildIndex].Evaluate(blackboard);

			// If the child is still running, return running and store the current state
			if (childStatus == TaskStatus.RUNNING)
			{
				return TaskStatus.RUNNING;
			}
			// If the child fails, return failure and reset the stored state
			else if (childStatus == TaskStatus.FAILURE)
			{
				storedState.Reset();
				return TaskStatus.FAILURE;
			}

			// Move to the next child
			storedState.currentChildIndex++;
		}

		storedState.Reset();
		return TaskStatus.SUCCESS;
	}
}
public class SequenceState
{
	public int currentChildIndex;

	public SequenceState(int currentChildIndex)
	{
		this.currentChildIndex = currentChildIndex;
	}

	public void Reset()
	{
		currentChildIndex = 0;
	}
}