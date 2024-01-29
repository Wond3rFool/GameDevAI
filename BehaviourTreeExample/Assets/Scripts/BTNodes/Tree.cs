using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tree : MonoBehaviour
{
    private TreeNode root = null;
    protected void Start()
    {
        root = SetupTree();
    }

    
    void Update()
    {
        if (root != null) 
        {
            root.Evaluate();
        }
    }
    protected abstract TreeNode SetupTree();
}
