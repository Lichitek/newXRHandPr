using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Inverter : Node
{
    protected List<Node> children = new List<Node>();

    public Inverter(List<Node> nodes)
    {
        this.children = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach (var node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.PLAY:
                    _nodeState = NodeState.PLAY;
                    break;
                case NodeState.RECORD:
                    _nodeState = NodeState.BASE;
                    break;
                case NodeState.BASE:
                    _nodeState = NodeState.RECORD;
                    break;
                default:
                    break;
            }
        }
        return _nodeState;
    }
}
