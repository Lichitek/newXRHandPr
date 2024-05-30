using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    protected List<Node> children = new List<Node>();

    public Selector(List<Node> nodes) 
    { 
        this.children = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach(var node in children)
        {
            switch(node.Evaluate())
            {
                case NodeState.PLAY:
                    _nodeState = NodeState.PLAY;
                    return _nodeState;
                case NodeState.RECORD: 
                    _nodeState = NodeState.RECORD;
                    return _nodeState;
                case NodeState.BASE:
                    break;
                default: 
                    break;
            }
        }
        _nodeState = NodeState.BASE;
        return _nodeState;
    }
}
