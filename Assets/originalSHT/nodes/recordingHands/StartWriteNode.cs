using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR.Samples;
using VIVE.OpenXR.Toolkits.CustomGesture;

public class StartWriteNode : Node
{
    private RecordingHands RH;
    private GameObject LHand;
    private GameObject RHand;
    private string cGName;


    public StartWriteNode(RecordingHands rh, GameObject handLeft, GameObject handRight, string cGName)
    {
        this.RH = rh;
        this.LHand = handLeft;
        this.RHand = handRight;
        this.cGName = cGName;
    }

    public override NodeState Evaluate()
    {
        if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Left))
        {
            LHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.yellow;
            RH.handTrack = HandFlag.Left;
            return NodeState.SUCCESS;
        }
        else if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Right))
        {
            RHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.yellow;
            RH.handTrack = HandFlag.Right;
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
