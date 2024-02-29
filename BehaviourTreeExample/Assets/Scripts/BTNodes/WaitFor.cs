using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitFor : BTBaseNode
{
	private float elapsedTime;
	private float timeToWait;

	public WaitFor(float timeToWait)
	{
		this.timeToWait = timeToWait;
	}

	public override TaskStatus Evaluate(Blackboard blackboard)
	{
		elapsedTime += Time.deltaTime;

		if (elapsedTime >= timeToWait)
		{
			elapsedTime = 0f;
			return TaskStatus.SUCCESS;
		}

		// Timer is still running
		return TaskStatus.RUNNING;
	}
}
