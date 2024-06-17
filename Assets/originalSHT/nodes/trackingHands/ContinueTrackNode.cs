using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using VIVE.OpenXR.Toolkits.CustomGesture;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using VIVE.OpenXR.Samples;
using static RecordingHands;
using System.Linq;

public class ContinueTrackNode : Node
{
    private TrackingHands tH;
    private string cGName;
    private LineRenderer lineRendererLeft;
    private LineRenderer lineRendererRight;
    private CkeckPoints[] ckeckPointsLeft;
    private CkeckPoints[] ckeckPointsRight;
    bool passed = false;
    int id = 0;
    public ContinueTrackNode(TrackingHands trackingHands, LineRenderer lineLeft, LineRenderer lineRight, string cGName)
    {
        this.tH = trackingHands;
        this.lineRendererLeft = lineLeft;  
        this.lineRendererRight = lineRight;
        this.cGName = cGName;

    }
    public override NodeState Evaluate()
    {
        if(passed)
        {
            id = 0;
            passed = false;
        }
        if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Left) && tH.ckeckVariant)
        {
            ckeckPointsLeft = lineRendererLeft.GetComponentsInChildren<CkeckPoints>();
            if (ckeckPointsLeft[id].pointPassed && ckeckPointsLeft.Length > id)
            {
                id++;
            }
            else if(ckeckPointsLeft.Length == id - 1)
            {
                lineRendererLeft.enabled = false;
                tH.PlayMethod(HandFlag.Left, tH.currentIndex);
                passed = true;
                return NodeState.SUCCESS;
            }

        }
        else if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Right) && tH.ckeckVariant)
        {
            ckeckPointsRight = lineRendererRight.GetComponentsInChildren<CkeckPoints>();
            if (ckeckPointsRight[id].pointPassed && ckeckPointsRight.Length > id)
            {
                id++;
            }
            else if (ckeckPointsRight.Length == id - 1)
            {
                lineRendererRight.enabled = false;
                tH.PlayMethod(HandFlag.Right, tH.currentIndex);
                passed = true;
                return NodeState.SUCCESS;
            }

        }
        return NodeState.PROCESS;
    }

}

