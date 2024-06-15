using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using System;
using VIVE.OpenXR.Toolkits.CustomGesture;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Unity.VisualScripting;
using VIVE.OpenXR.Samples;


public class recordingHands : MonoBehaviour
{
    [System.Serializable]
    public class ListHands
    {
        public List<JointsCoordinats> listHands = new List<JointsCoordinats>();
    }
    [System.Serializable]
    public class JointsCoordinats
    {
        public List<Coordinats> jointsCoordinats = new List<Coordinats>();
    }

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
    private string customGestureName;
    [SerializeField]
    private int counterL;
    [SerializeField]
    private int counterR;


    [SerializeField] private CustomGestureManager HGM;
    [SerializeField] private CustomGestureDefiner GD;
    [SerializeField] private LineRenderer lineRenderer;

    bool checkCor = false; 

    private Node topNode;

    private void Awake()
    {
        lineRenderer.enabled = false;
        GD = FindAnyObjectByType<CustomGestureDefiner>();
        HGM = FindAnyObjectByType<CustomGestureManager>();
        leftHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
        rightHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
        lineRenderer.positionCount = (int)timerOrig;
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
        StartWriteNode startWriteNode = new StartWriteNode(this, leftHand, rightHand, customGestureName);
        ContinueWriteNode continueWriteNode = new ContinueWriteNode(framesJC, this, customGestureName);
        EndWriteNode endWriteNode = new EndWriteNode(leftHand, rightHand);
        Sequence mainSelector = new Sequence(new List<Node> { startWriteNode, continueWriteNode, endWriteNode });
        topNode = new Selector(new List<Node> { mainSelector });
    }

    void Update()
    {
        topNode.Evaluate();
        if (topNode.nodeState == NodeState.PROCESS && !checkCor)
        {     
            StartCoroutine(RecJointsLeft());
            checkCor = true;
        }
    }
    public IEnumerator RecJointsLeft()
    {
        lineRenderer.enabled = true;
        int num = 0;
        while (num < timerOrig)
        {
            framesJC.jointsCoordinats[num].posX = HandTracking.GetHandJointLocations(HandFlag.Left)[0].position.x;
            framesJC.jointsCoordinats[num].posY = HandTracking.GetHandJointLocations(HandFlag.Left)[0].position.y;
            framesJC.jointsCoordinats[num].posZ = HandTracking.GetHandJointLocations(HandFlag.Left)[0].position.z;
            framesJC.jointsCoordinats[num].rotW = HandTracking.GetHandJointLocations(HandFlag.Left)[0].rotation.w;
            framesJC.jointsCoordinats[num].rotX = HandTracking.GetHandJointLocations(HandFlag.Left)[0].rotation.x;
            framesJC.jointsCoordinats[num].rotY = HandTracking.GetHandJointLocations(HandFlag.Left)[0].rotation.y;
            framesJC.jointsCoordinats[num].rotZ = HandTracking.GetHandJointLocations(HandFlag.Left)[0].rotation.z;
            for(int i = num; i < timerOrig; i++)
            {
                lineRenderer.SetPosition(i, HandTracking.GetHandJointLocations(HandFlag.Left)[0].position);
            }            
            yield return new WaitForSeconds(1.0f);
            num++;
        }
        RecFileLeft();
    }
    public void RecFileLeft()
    {
        lineRenderer.enabled = false;
        string json = JsonUtility.ToJson(framesJC);
        string filepath = Application.dataPath + "/Resources/Left/" + fileName + ".json";
        Debug.Log(filepath);
        System.IO.File.WriteAllText(filepath, json);
        Debug.Log("node2 SUCCESS");
        checkCor = true;
    }
    public IEnumerator RecJointsRight()
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
    }
    public void RecFileRight()
    {
        lineRenderer.enabled = false;
        string json = JsonUtility.ToJson(framesJC);
        string filepath = Application.dataPath + "/Resources/Right/" + fileName + ".json";
        Debug.Log(filepath);
        System.IO.File.WriteAllText(filepath, json);
        Debug.Log("node2 SUCCESS");
    }


}
