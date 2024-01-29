using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TaskStatus { Success, Failed, Running }
public class TreeNode
{
    public TreeNode parent;
    protected TaskStatus status;
    protected List<TreeNode> children = new List<TreeNode>();

    private Dictionary<string, object> dataContext = new Dictionary<string, object>();

    public TreeNode()
    {
        parent = null;
    }

    public TreeNode(List<TreeNode> children)
    {
        foreach (TreeNode child in children)
        {
            _Attach(child);
        }
    }

    private void _Attach(TreeNode node)
    {
        node.parent = this;
        children.Add(node);
    }

    public virtual TaskStatus Evaluate() => TaskStatus.Failed;

    public void SetData(string key, object value)
    {
        dataContext[key] = value;
    }

    public object GetData(string key)
    {
        object value = null;
        if (dataContext.TryGetValue(key, out value))
            return value;

        TreeNode node = parent;
        while (node != null)
        {
            value = node.GetData(key);
            if (value != null)
            {
                return value;
            }
            node = node.parent;
        }
        return null;
    }

    public bool ClearData(string key)
    {
        if (dataContext.ContainsKey(key))
        {
            dataContext.Remove(key);
            return true;
        }

        TreeNode node = parent;
        while (node != null)
        {
            bool cleared = node.ClearData(key);
            if (cleared)
            {
                return true;
            }
            node = node.parent;
        }
        return false;
    }
}

