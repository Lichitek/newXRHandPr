using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using System;
using VIVE.OpenXR.Toolkits.CustomGesture;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Unity.VisualScripting;
using VIVE.OpenXR.Samples;


public class RecordingHands : MonoBehaviour
{
    [HideInInspector]
    [System.Serializable]
    public class ListHands
    {
        public List<JointsCoordinats> listHands = new List<JointsCoordinats>();
    }
    [HideInInspector]
    [System.Serializable]
    public class JointsCoordinats
    {
        public List<Coordinats> jointsCoordinats = new List<Coordinats>();
    }
    [HideInInspector]
    [System.Serializable]
    public class Coordinats
    {
        public float posX;
        public float posY;
        public float posZ;
        public float rotW;
        public float rotX;
        public float rotY;
        public float rotZ;
    }

    [SerializeField]
    public JointsCoordinats framesJC;
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private string fileName;
    [SerializeField]
    private float timerOrig;
    [SerializeField]
    private string customGestureNameStart;
    [SerializeField]
    private string customGestureNameEnd;
    [SerializeField] 
    private LineRenderer lineRendererLeft;
    [SerializeField]
    private LineRenderer lineRendererRight;

    private Node topNode;
    public HandFlag handTrack;
    public bool checkCoroutine = false; 

    private void Awake()
    {
        lineRendererLeft.enabled = false;
        lineRendererLeft.positionCount = (int)timerOrig;
        lineRendererRight.enabled = false;
        lineRendererRight.positionCount = (int)timerOrig;
        leftHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
        rightHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
        for(int i = 0; i < timerOrig; i++)
        {
            framesJC.jointsCoordinats.Add(new Coordinats());
        }
    }
    private void Start()
    {
        ConstructBehahaviourTree();
    }
    private void ConstructBehahaviourTree()
    {
        StartWriteNode startWriteNode = new StartWriteNode(this, leftHand, rightHand, customGestureNameStart);
        ContinueWriteNode continueWriteNode = new ContinueWriteNode(customGestureNameStart);
        EndWriteNode endWriteNode = new EndWriteNode(this, leftHand, rightHand, customGestureNameEnd);
        Sequence mainSelector = new Sequence(new List<Node> { startWriteNode, continueWriteNode, endWriteNode });
        topNode = new Selector(new List<Node> { mainSelector });
    }

    void Update()
    {
        topNode.Evaluate();
        if (topNode.nodeState == NodeState.PROCESS && !checkCoroutine)
        {     
            StartCoroutine(RecJoints(handTrack));
            checkCoroutine = true;
        }
    }
    public IEnumerator RecJoints(HandFlag handFlag)
    {
        if(handFlag == HandFlag.Left)
            lineRendererLeft.enabled = true;
        else if (handFlag == HandFlag.Right)
            lineRendererRight.enabled = true;
        int num = 0;
        while (num < timerOrig)
        {
            framesJC.jointsCoordinats[num].posX = HandTracking.GetHandJointLocations(handFlag)[0].position.x;
            framesJC.jointsCoordinats[num].posY = HandTracking.GetHandJointLocations(handFlag)[0].position.y;
            framesJC.jointsCoordinats[num].posZ = HandTracking.GetHandJointLocations(handFlag)[0].position.z;
            framesJC.jointsCoordinats[num].rotW = HandTracking.GetHandJointLocations(handFlag)[0].rotation.w;
            framesJC.jointsCoordinats[num].rotX = HandTracking.GetHandJointLocations(handFlag)[0].rotation.x;
            framesJC.jointsCoordinats[num].rotY = HandTracking.GetHandJointLocations(handFlag)[0].rotation.y;
            framesJC.jointsCoordinats[num].rotZ = HandTracking.GetHandJointLocations(handFlag)[0].rotation.z;
            if (handFlag == HandFlag.Left)
                for (int i = num; i < timerOrig; i++)
                {
                    lineRendererLeft.SetPosition(i, HandTracking.GetHandJointLocations(handFlag)[0].position);
                }
            else if (handFlag == HandFlag.Right)
                for (int i = num; i < timerOrig; i++)
                {
                    lineRendererRight.SetPosition(i, HandTracking.GetHandJointLocations(handFlag)[0].position);
                }
            yield return new WaitForSeconds(1.0f);
            num++;
        }
        RecFile(handFlag);
    }
    public void RecFile(HandFlag handFlag)
    {
        switch (handFlag)
        {
            case HandFlag.Left:
                lineRendererLeft.enabled = false;
                string jsonLeft = JsonUtility.ToJson(framesJC);
                string filepathLeft = Application.dataPath + "/Resources/Left/" + fileName + ".json";
                System.IO.File.WriteAllText(filepathLeft, jsonLeft);
                break;
            case HandFlag.Right:
                lineRendererRight.enabled = false;
                string jsonRight = JsonUtility.ToJson(framesJC);
                string filepathRight = Application.dataPath + "/Resources/Left/" + fileName + ".json";
                System.IO.File.WriteAllText(filepathRight, jsonRight);
                break;
        }
    }
    /*public IEnumerator RecJointsRight()
    {
        lineRenderer.enabled = true;
        int num = 0;
        while (num < 30)
        {
            framesJC.jointsCoordinats[num].posX = HandTracking.GetHandJointLocations(HandFlag.Left)[0].position.x;
            framesJC.jointsCoordinats[num].posY = HandTracking.GetHandJointLocations(HandFlag.Left)[0].position.y;
            framesJC.jointsCoordinats[num].posZ = HandTracking.GetHandJointLocations(HandFlag.Left)[0].position.z;
            framesJC.jointsCoordinats[num].rotW = HandTracking.GetHandJointLocations(HandFlag.Left)[0].rotation.w;
            framesJC.jointsCoordinats[num].rotX = HandTracking.GetHandJointLocations(HandFlag.Left)[0].rotation.x;
            framesJC.jointsCoordinats[num].rotY = HandTracking.GetHandJointLocations(HandFlag.Left)[0].rotation.y;
            framesJC.jointsCoordinats[num].rotZ = HandTracking.GetHandJointLocations(HandFlag.Left)[0].rotation.z;
            lineRenderer.SetPosition(num, HandTracking.GetHandJointLocations(HandFlag.Right)[0].position);
            Debug.Log("node2 PROCESS in class = " + num);
            num++;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        RecFileRight();
    }*/
    /*public void RecFileRight()
    {
        lineRenderer.enabled = false;
        string json = JsonUtility.ToJson(framesJC);
        string filepath = Application.dataPath + "/Resources/Right/" + fileName + ".json";
        Debug.Log(filepath);
        System.IO.File.WriteAllText(filepath, json);
        Debug.Log("node2 SUCCESS");
    }*/


}
