using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR.Toolkits.CustomGesture;

public class EndWriteNode : Node
{
    private GameObject LHand;
    private GameObject RHand;

    public EndWriteNode(GameObject handLeft, GameObject handRight)
    {
        this.LHand = handLeft;
        this.RHand = handRight;
    }

    public override NodeState Evaluate()
    {
        if (CustomGestureDefiner.IsCurrentGestureTriiggered("Five", CGEnums.HandFlag.Left))
        {
            LHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
            return NodeState.PROCESS;
        }
        else if (CustomGestureDefiner.IsCurrentGestureTriiggered("Five", CGEnums.HandFlag.Right))
        {
            RHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
            return NodeState.PROCESS;
        }
        
        return NodeState.PROCESS;
    }
}
