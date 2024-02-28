using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inverter : BTBaseNode
{
	private BTBaseNode child;

	public Inverter(BTBaseNode child)
	{
		this.child = child;
	}

	public override TaskStatus Evaluate(Blackboard blackboard)
	{
		switch (child.Evaluate(blackboard))
		{
			case TaskStatus.SUCCESS:
				return TaskStatus.FAILURE;
			case TaskStatus.FAILURE:
				return TaskStatus.SUCCESS;
			case TaskStatus.RUNNING:
				return TaskStatus.RUNNING;
			default:
				// Handle any unexpected states, if necessary
				return TaskStatus.FAILURE;
		}
	}
}

