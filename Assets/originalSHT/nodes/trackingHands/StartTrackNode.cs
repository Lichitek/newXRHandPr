using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR.Samples;
using VIVE.OpenXR.Toolkits.CustomGesture;

public class StartTrackNode : Node
{
    private GameObject LH;
    private GameObject RH;
    private string cGName;
    private trackingHands tH;

    public StartTrackNode(trackingHands trackingHands, GameObject handLeft, GameObject handRight, string cGName)
    {
        this.tH = trackingHands;
        this.LH = handLeft;
        this.RH = handRight;
        this.cGName = cGName;
    }

    public override NodeState Evaluate()
    {
        //if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Left))
        //{
        //    LH.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.green;
        //    //tH.currentFlag = HandFlag.Left;
        //    Debug.Log("node1 left SUCCESS");
        //    return NodeState.SUCCESS;
        //}
        //else if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Right))
        //{
        //    RH.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.green;
        //    //tH.currentFlag |= HandFlag.Right;
        //    Debug.Log("node1 right SUCCESS");
        //    return NodeState.SUCCESS;
        //}
        return NodeState.SUCCESS;
    }
}
