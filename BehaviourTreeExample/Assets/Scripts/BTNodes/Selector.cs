using System.Collections.Generic;

public class Selector : TreeNode
{
    public Selector() : base() { }
    public Selector(List<TreeNode> children) : base(children) { }

    public override TaskStatus Evaluate()
    {
        foreach (TreeNode child in children)
        {
            switch (child.Evaluate())
            {
                case TaskStatus.Failed:
                    continue;
                case TaskStatus.Success:
                    status = TaskStatus.Success;
                    return status;
                case TaskStatus.Running:
                    status = TaskStatus.Running;
                    // Keep iterating through other children to check for additional running tasks
                    break;
            }
        }

        status = TaskStatus.Failed;
        return status;
    }
}
