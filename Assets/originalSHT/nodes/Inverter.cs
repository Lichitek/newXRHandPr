using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Inverter : Node
{
    protected Node node;

    public Inverter(Node nodes)
    {
        this.node = nodes;
    }

    public override NodeState Evaluate()
    {

        switch (node.Evaluate())
        {
            case NodeState.PROCESS:
                _nodeState = NodeState.PROCESS;
                break;
            case NodeState.SUCCESS:
                _nodeState = NodeState.FAILURE;
                break;
            case NodeState.FAILURE:
                _nodeState = NodeState.SUCCESS;
                break;
            default:
                break;
        }
        
        return _nodeState;
    }
    public override IEnumerable recJoints()
    {
        yield return 0;
    }
}
