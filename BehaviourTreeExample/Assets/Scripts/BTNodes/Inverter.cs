using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inverter : TreeNode
{
	private TreeNode child;

	public Inverter(TreeNode child)
	{
		this.child = child;
	}

	public override TaskStatus Evaluate()
	{
		switch (child.Evaluate())
		{
			case TaskStatus.Success:
				return TaskStatus.Failed;
			case TaskStatus.Failed:
				return TaskStatus.Success;
			case TaskStatus.Running:
				return TaskStatus.Running;
			default:
				// Handle any unexpected states, if necessary
				return TaskStatus.Failed;
		}
	}
}

