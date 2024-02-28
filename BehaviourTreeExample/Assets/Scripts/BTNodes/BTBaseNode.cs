using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskStatus { SUCCESS, FAILURE, RUNNING, UNKNOWN }

/// <summary>
/// Base class for all nodes in the behaviour tree.
/// </summary>
public abstract class BTBaseNode
{
	protected TaskStatus state;

	public BTBaseNode parent;

	protected List<BTBaseNode> children = new List<BTBaseNode>();

	public BTBaseNode()
	{
		
	}

	public BTBaseNode(List<BTBaseNode> children)
	{
		foreach (BTBaseNode child in children)
		{
			_Attach(child);
		}
	}
	private void _Attach(BTBaseNode node)
	{
		node.parent = this;
		children.Add(node);
	}
	public abstract TaskStatus Evaluate(Blackboard blackboard);

	public virtual void Reset() { state = TaskStatus.UNKNOWN; }
	public virtual void Terminate() { state = TaskStatus.UNKNOWN; }
}
