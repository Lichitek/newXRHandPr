using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using VIVE.OpenXR.Toolkits.CustomGesture;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using VIVE.OpenXR.Samples;
using static RecordingHands;
using System.Linq;

public class SetTrackNode : Node
{
    private TrackingHands tH;
    private HandFlag HF;
    private ListHands listLH;
    private ListHands listRH;
    private string cGName;

    public SetTrackNode(TrackingHands trackingHands, HandFlag handFlag, ListHands listL, ListHands listR, string cGName)
    {
        this.tH = trackingHands;
        this.HF = handFlag;
        this.listLH = listL;
        this.listRH = listR;
        this.cGName = cGName;
    }

    public override NodeState Evaluate()
    {
        if(!tH.ckeckShow)
            tH.ShowVariants(HF);
        if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Left))
        {
            for(int i = 0; i < listLH.listHands.Count; i++)
            {
                if (HandTracking.GetHandJointLocations(HF)[0].position.y == listLH.listHands[i].jointsCoordinats[0].posY 
                    && HandTracking.GetHandJointLocations(HF)[0].position.z == listLH.listHands[i].jointsCoordinats[0].posZ
                    && !tH.ckeckVariant)
                {
                    tH.HideVariants(HF, i);
                    tH.DrowTrajectory(HF, i);
                    tH.currentIndex = i;
                    tH.ckeckVariant = true;
                    return NodeState.SUCCESS;
                }
            }
        }
        else if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Right))
        {
            for (int i = 0; i < listRH.listHands.Count; i++)
            {
                if (HandTracking.GetHandJointLocations(HF)[0].position.y == listRH.listHands[i].jointsCoordinats[0].posY
                    && HandTracking.GetHandJointLocations(HF)[0].position.z == listRH.listHands[i].jointsCoordinats[0].posZ
                    && !tH.ckeckVariant)
                {
                    tH.HideVariants(HF, i);
                    tH.DrowTrajectory(HF, i);
                    tH.currentIndex = i;
                    tH.ckeckVariant = true;
                    return NodeState.SUCCESS;
                }
            }
        }
        return NodeState.PROCESS;
    }
}
