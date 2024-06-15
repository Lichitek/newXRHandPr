using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using VIVE.OpenXR.Toolkits.CustomGesture;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using VIVE.OpenXR.Samples;
using static recordingHands;

public class SetWriteNode : Node
{
    private trackingHands tH;
    private ListHands recLeft;
    private ListHands recRight;
    private string cGName;

    public SetWriteNode(trackingHands trackingHands, ListHands recLeft, ListHands recRight, string cGName)
    {
        this.tH = trackingHands;
        this.recLeft = recLeft;
        this.recRight = recRight;
        this.cGName = cGName;
    }

    public override NodeState Evaluate()
    {
        //if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Left))
        //{
        //    for (int i = 0; i < recLeft.listHands.Count; i++)
        //    {
        //        if (HandTracking.GetHandJointLocations(HandFlag.Left)[0].position.x == recLeft.listHands[i].jointsCoordinats[0].posX)
        //        {
        //            tH.currentFlag = HandFlag.Left;
        //            tH.currentIndex = i;
        //            return NodeState.PROCESS;
        //        }
        //    }
        //}
        //else if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Right))
        //{
        //    for (int i = 0; i < recRight.listHands.Count; i++)
        //    {
        //        if (HandTracking.GetHandJointLocations(HandFlag.Right)[0].position.x == recRight.listHands[i].jointsCoordinats[0].posX)
        //        {
        //            tH.currentFlag = HandFlag.Right;
        //            tH.currentIndex = i;
        //            return NodeState.PROCESS;
        //        }
        //    }
        //}
        //Debug.Log("node2 PROCESS");
        return NodeState.SUCCESS;
    }
}
