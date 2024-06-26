using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR.Toolkits.CustomGesture;
using VIVE.OpenXR.Samples;

public class EndWriteNode : Node
{
    private RecordingHands RH;
    private GameObject LHand;
    private GameObject RHand;
    private string cGName;

    public EndWriteNode(RecordingHands rh, GameObject handLeft, GameObject handRight, string cGName)
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
            LHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
            RH.handTrack = HandFlag.None;
            RH.checkCoroutine = false;
            return NodeState.SUCCESS;
        }
        else if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Right))
        {
            RHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
            RH.handTrack = HandFlag.None;
            RH.checkCoroutine = false;
            return NodeState.SUCCESS;
        }
        return NodeState.PROCESS;
    }
}
