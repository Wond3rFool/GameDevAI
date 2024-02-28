using System.Collections.Generic;

public class Selector : BTBaseNode
{
	public Selector() : base() { }
	public Selector(List<BTBaseNode> children) : base(children) { }

	private int currentChildIndex = 0;
	public override TaskStatus Evaluate(Blackboard blackboard)
	{
		while (currentChildIndex < children.Count)
		{
			BTBaseNode child = children[currentChildIndex];
			TaskStatus childState = child.Evaluate(blackboard);

			switch (childState)
			{
				case TaskStatus.SUCCESS:
					state = TaskStatus.SUCCESS;
					currentChildIndex = 0;  // Reset to the first child for the next iteration
					return state;
				case TaskStatus.FAILURE:
					currentChildIndex++;  // Move on to the next child
					continue;
				case TaskStatus.RUNNING:
					state = TaskStatus.RUNNING;
					return state;
				default:
					// Handle any unexpected states, if necessary
					continue;
			}
		}

		// If all children have been attempted and none succeeded, return FAILURE
		state = TaskStatus.FAILURE;
		currentChildIndex = 0;  // Reset to the first child for the next iteration
		return state;
	}
}
