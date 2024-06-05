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

public class continueWriteNode : Node
{
    private JointsCoordinats jointsCoordinats;
    private List<CustomGesture> cG;
    private string fileName;
    private float timer;
    private int num = 0;
    private LineRenderer lineRenderer;

    public continueWriteNode(JointsCoordinats JC, List<CustomGesture> cG, LineRenderer lineRenderer, float timer, string fN)
    {
        this.jointsCoordinats = JC;
        this.cG = cG;
        this.timer = timer;
        this.fileName = fN;
    }

    public override NodeState Evaluate()
    {

        foreach (CustomGesture _Gestures in cG)
        {
            if (CustomGestureDefiner.IsCurrentGestureTriiggered(_Gestures.GestureName, CGEnums.HandFlag.Left))
            {
                if (timer > 0)
                {
                    Debug.Log("node2 PROCESS");
                    recJoints();
                    return NodeState.PROCESS;
                }
                else if (timer <= 0)
                {
                    string json = JsonUtility.ToJson(jointsCoordinats);
                    string filepath = Application.dataPath + "/Resourses/Left/" + fileName + ".json";
                    Debug.Log(filepath);
                    System.IO.File.WriteAllText(filepath, json);
                    Debug.Log("node2 SUCCESS");
                    return NodeState.SUCCESS;
                }
            }
            else if (CustomGestureDefiner.IsCurrentGestureTriiggered(_Gestures.GestureName, CGEnums.HandFlag.Right))
            {
                if (_Gestures.GestureName == "Five" && timer > 0)
                {
                    lineRenderer.SetPosition(num, HandTracking.GetHandJointLocations(HandFlag.Left)[0].position);
                    jointsCoordinats.jointsCoord[num].posX = HandTracking.GetHandJointLocations(HandFlag.Right)[0].position.x;
                    jointsCoordinats.jointsCoord[num].posY = HandTracking.GetHandJointLocations(HandFlag.Right)[0].position.y;
                    jointsCoordinats.jointsCoord[num].posZ = HandTracking.GetHandJointLocations(HandFlag.Right)[0].position.z;
                    jointsCoordinats.jointsCoord[num].rotW = HandTracking.GetHandJointLocations(HandFlag.Right)[0].rotation.w;
                    jointsCoordinats.jointsCoord[num].rotX = HandTracking.GetHandJointLocations(HandFlag.Right)[0].rotation.x;
                    jointsCoordinats.jointsCoord[num].rotY = HandTracking.GetHandJointLocations(HandFlag.Right)[0].rotation.y;
                    jointsCoordinats.jointsCoord[num].rotZ = HandTracking.GetHandJointLocations(HandFlag.Right)[0].rotation.z;
                    num++;
                    timer -= Time.deltaTime;
                    Debug.Log("node2 PROCESS");
                    return NodeState.PROCESS;
                }
                else if (timer <= 0)
                {
                    string json = JsonUtility.ToJson(jointsCoordinats);
                    string filepath = Application.dataPath + "/Resourses/Right/" + fileName + ".json";
                    Debug.Log(filepath);
                    System.IO.File.WriteAllText(filepath, json);
                    Debug.Log("node2 SUCCESS");
                    return NodeState.SUCCESS;
                }
            }
        }
        Debug.Log("node2 PROCESS");
        return NodeState.PROCESS;
    }

    public override IEnumerable recJoints()
    {
        //lineRenderer.SetPosition(num, HandTracking.GetHandJointLocations(HandFlag.Left)[0].position);
        jointsCoordinats.jointsCoord[num].posX = HandTracking.GetHandJointLocations(HandFlag.Left)[0].position.x;
        jointsCoordinats.jointsCoord[num].posY = HandTracking.GetHandJointLocations(HandFlag.Left)[0].position.y;
        jointsCoordinats.jointsCoord[num].posZ = HandTracking.GetHandJointLocations(HandFlag.Left)[0].position.z;
        jointsCoordinats.jointsCoord[num].rotW = HandTracking.GetHandJointLocations(HandFlag.Left)[0].rotation.w;
        jointsCoordinats.jointsCoord[num].rotX = HandTracking.GetHandJointLocations(HandFlag.Left)[0].rotation.x;
        jointsCoordinats.jointsCoord[num].rotY = HandTracking.GetHandJointLocations(HandFlag.Left)[0].rotation.y;
        jointsCoordinats.jointsCoord[num].rotZ = HandTracking.GetHandJointLocations(HandFlag.Left)[0].rotation.z;
        num++;
        timer -= Time.deltaTime;
        Debug.Log("node2 PROCESS = " + timer);

        
        yield return new WaitForSecondsRealtime(0.1f);
    }
}
