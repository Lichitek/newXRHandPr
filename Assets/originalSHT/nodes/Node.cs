using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public abstract class Node
{
    protected NodeState _nodeState;
    public NodeState nodeState {  get { return _nodeState; } }

    public abstract NodeState Evaluate();
}

public enum NodeState
{
    RECORD, PLAY, BASE
}
