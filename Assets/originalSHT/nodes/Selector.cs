using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

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
                case NodeState.PROCESS:
                    _nodeState = NodeState.PROCESS;
                    return _nodeState;
                case NodeState.SUCCESS: 
                    _nodeState = NodeState.SUCCESS;
                    return _nodeState;
                case NodeState.FAILURE:
                    break;
                default: 
                    break;
            }
        }
        _nodeState = NodeState.FAILURE;
        return _nodeState;
    }
    public override IEnumerable recJoints()
    {
        yield return 0;
    }
}
