using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayText : BTBaseNode
{
    private TextMeshPro text;
    string toDisplay;

    public DisplayText(TextMeshPro text, string toDisplay) 
    {
        this.text = text;
        this.toDisplay = toDisplay; 
    }

    public override TaskStatus Evaluate(Blackboard blackboard)
    {
        text.text = toDisplay;
        Debug.Log(text.transform.parent.name+ ": "+text.text);
        return TaskStatus.SUCCESS;
    }

}
