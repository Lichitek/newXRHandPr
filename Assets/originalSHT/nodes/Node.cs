using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[System.Serializable]
public abstract class Node
{
    protected NodeState _nodeState;
    public NodeState nodeState {  get { return _nodeState; } }

    public abstract NodeState Evaluate();
    public abstract IEnumerable recJoints();
}

public enum NodeState
{
    PROCESS, SUCCESS, FAILURE
}
