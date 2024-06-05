using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR.Toolkits.CustomGesture;

public class endWriteNode : Node
{
    private List<CustomGesture> cG;
    private GameObject LH;
    private GameObject RH;

    public endWriteNode(List<CustomGesture> cG, GameObject handLeft, GameObject handRight)
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
                if (_Gestures.GestureName == "OK")
                {
                    LH.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
                    Debug.Log("node3  SUCCESS");
                    return NodeState.SUCCESS;
                }
                    
            }
            else if (CustomGestureDefiner.IsCurrentGestureTriiggered(_Gestures.GestureName, CGEnums.HandFlag.Right))
            {
                if (_Gestures.GestureName == "OK")
                {
                    RH.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
                    Debug.Log("node3 SUCCESS");
                    return NodeState.SUCCESS;
                }

            }
        }
        Debug.Log("node3 PROCESS");
        return NodeState.PROCESS;
    }
    public override IEnumerable recJoints()
    {
        yield return 0;
    }
}
