using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tree : MonoBehaviour
{
    private BTBaseNode root = null;

    private Blackboard blackboard;
    public Blackboard Blackboard { get { return blackboard; } set { blackboard = value; } }

    protected virtual void Awake()
    {
        blackboard = new Blackboard();
    }
    protected void Start()
    {
        root = SetupTree();
    }

    void Update()
    {
        if (root != null) 
        {
            root.Evaluate(blackboard);
        }
    }
    protected abstract BTBaseNode SetupTree();
}
