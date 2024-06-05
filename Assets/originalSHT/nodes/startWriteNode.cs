using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR.Samples;
using VIVE.OpenXR.Toolkits.CustomGesture;

public class startWriteNode : Node
{
    private List<CustomGesture> cG;
    private GameObject LH;
    private GameObject RH;

    public startWriteNode(List<CustomGesture> cG, GameObject handLeft, GameObject handRight)
    {
        this.cG = cG;
        this.LH = handLeft;
        this.RH = handRight;
    }

    public override NodeState Evaluate()
    {
        foreach (CustomGesture _Gestures in cG)
        {
            if (CustomGestureDefiner.IsCurrentGestureTriiggered(_Gestures.GestureName, CGEnums.HandFlag.Left))
            {
                if (_Gestures.GestureName == "Fist")
                {
                    LH.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.yellow;
                    Debug.Log("node1 SUCCESS");
                    return NodeState.SUCCESS;
                }
                    
            }
            else if (CustomGestureDefiner.IsCurrentGestureTriiggered(_Gestures.GestureName, CGEnums.HandFlag.Right))
            {
                if (_Gestures.GestureName == "Fist")
                {
                    RH.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.yellow;
                    Debug.Log("node1 SUCCESS");
                    return NodeState.SUCCESS;
                }
            }
        }
        return NodeState.PROCESS;
    }
    public override IEnumerable recJoints()
    {
        yield return 0;
    }
}
