using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using VIVE.OpenXR.Samples;
using VIVE.OpenXR.Toolkits.CustomGesture;

public class StartTrackNode : Node
{
    private TrackingHands tH;
    private List<string> listLH = new List<string>();
    private List<string> listRH = new List<string>();
    private GameObject LH;
    private GameObject RH;
    private string cGName;

    public StartTrackNode(TrackingHands trackingHands, List<string> lHL, List<string> lHR, GameObject handLeft, GameObject handRight, string cGName)
    {
        this.tH = trackingHands;
        this.listLH = lHL;
        this.listRH = lHR;
        this.LH = handLeft;
        this.RH = handRight;
        this.cGName = cGName;
    }

    public override NodeState Evaluate()
    {
        if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Left))
        {
            LH.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.green;
            tH.currentFlag = HandFlag.Left;
            IEnumerable<string> filesLeft = Directory.EnumerateFiles(Application.dataPath + "/Resources/Left/", "*.json");
            if (filesLeft.Count() != listLH.Count)
                foreach (string filename in filesLeft)
                {
                    if(!listLH.Contains(filename))
                        listLH.Add(filename);
                }
            return NodeState.SUCCESS;
        }
        else if (CustomGestureDefiner.IsCurrentGestureTriiggered(cGName, CGEnums.HandFlag.Right))
        {
            RH.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.green;
            tH.currentFlag = HandFlag.Right;
            IEnumerable<string> filesRight = Directory.EnumerateFiles(Application.dataPath + "/Resources/Right/", "*.json");
            if (filesRight.Count() != listRH.Count)
                foreach (string filename in filesRight)
                {
                    if(!listRH.Contains(filename))
                        listRH.Add(filename);
                }
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}

