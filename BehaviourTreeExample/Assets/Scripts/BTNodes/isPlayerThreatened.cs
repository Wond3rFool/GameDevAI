using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isPlayerThreatened : BTBaseNode
{


    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        if (Player.beingAttacked) 
        {
            return TaskStatus.SUCCESS;
        }
        return TaskStatus.FAILURE;
    }
}
