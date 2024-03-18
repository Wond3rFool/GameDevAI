using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isPlayerThreatened : BTBaseNode
{
    private GameObject gameObject;

    public isPlayerThreatened(GameObject gameObject) 
    {
        this.gameObject = gameObject;
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {

        if (gameObject.GetComponent<Player>().IsBeingAttacked()) 
        {
            return TaskStatus.SUCCESS;
        }
        return TaskStatus.FAILURE;
    }
}
