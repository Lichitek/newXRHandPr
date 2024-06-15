using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Sequence : Node
{
    protected List<Node> children = new List<Node>();

    public Sequence(List<Node> nodes)
    {
        this.children = nodes;
    }

    public override NodeState Evaluate()
    {
        bool isNodeRun  = false;
        foreach (var node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.PROCESS:
                    isNodeRun = true;
                    break;
                case NodeState.SUCCESS:
                    break;
                case NodeState.FAILURE:
                    _nodeState = NodeState.FAILURE;
                    return _nodeState;
                default:
                    break;
            }
        }
        _nodeState = isNodeRun ? NodeState.PROCESS : NodeState.SUCCESS;
        return _nodeState;
    }
}
