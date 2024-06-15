using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR.Toolkits.CustomGesture;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System.IO;
using static Unity.Burst.Intrinsics.X86;
using System;
using VIVE.OpenXR.Hand;
using VIVE.OpenXR.Samples.Hand;
using VIVE.OpenXR.Samples;
using System.Xml.Linq;
using UnityEngine.XR;
using System.Linq;
using static recordingHands;

public class ContinueWriteNode : Node
{
    private recordingHands hands;
    private JointsCoordinats jointsCoordinats;
    private string cGName;
    public ContinueWriteNode(JointsCoordinats JC, recordingHands hands, string cGName)
    {
        this.jointsCoordinats = JC;
        this.hands = hands;
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
