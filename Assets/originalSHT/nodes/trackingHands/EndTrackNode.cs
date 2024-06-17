using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR.Toolkits.CustomGesture;
using VIVE.OpenXR.Samples;

public class EndTrackNode : Node
{
    private TrackingHands tH;
    private GameObject LHand;
    private GameObject RHand;
    private string cGName;

    public EndTrackNode(TrackingHands tH, GameObject handLeft, GameObject handRight, string cGName)
    {
        this.tH = tH;
        this.LHand = handLeft;
        this.RHand = handRight;
        this.cGName = cGName;
    }

    public override NodeState Evaluate()
    {
        if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Left) && tH.ckeckVariant)
        {
            LHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
            tH.currentFlag = HandFlag.None;
            tH.currentIndex = -1;
            tH.ckeckShow = false;
            tH.ckeckVariant = false;
            return NodeState.SUCCESS;
        }
        else if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Right) && tH.ckeckVariant)
        {
            RHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
            tH.currentFlag = HandFlag.None;
            tH.currentIndex = -1;
            tH.ckeckShow = false;
            tH.ckeckVariant = false;
            return NodeState.SUCCESS;
        }
        return NodeState.PROCESS;
    }
}
