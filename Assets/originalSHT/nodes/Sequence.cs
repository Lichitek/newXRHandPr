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
                case NodeState.PLAY:
                    isNodeRun = true;
                    break;
                case NodeState.RECORD:
                    break;
                case NodeState.BASE:
                    _nodeState = NodeState.BASE;
                    return _nodeState;
                default:
                    break;
            }
        }
        _nodeState = isNodeRun ? NodeState.RECORD : NodeState.PLAY;
        return _nodeState;
    }
}
