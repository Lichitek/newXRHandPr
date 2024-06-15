using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using VIVE.OpenXR.Samples;

[System.Serializable]
public abstract class Node
{
    protected NodeState _nodeState;
    public NodeState nodeState {  get { return _nodeState; } }
    

    public abstract NodeState Evaluate();
}

public enum NodeState
{
    PROCESS, SUCCESS, FAILURE,
}
