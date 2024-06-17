using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR.Samples;
using VIVE.OpenXR.Toolkits.CustomGesture;

public class ContinueWriteNode : Node
{
    private string cGName;
    public ContinueWriteNode(string cGName)
    {
        this.cGName = cGName;
    }

    public override NodeState Evaluate()
    {
        if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Left))
        {
            return NodeState.PROCESS;
        }
        else if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Right))
        {
            return NodeState.PROCESS;
        }
        return NodeState.FAILURE;
    }

}
