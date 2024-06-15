using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using VIVE.OpenXR.Samples;
using static recordingHands;

public class trackingHands : MonoBehaviour
{
    [SerializeField]
    List<string> listHandsLeft = new List<string>();
    [SerializeField]
    List<string> listHandsRight = new List<string>();
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private ListHands recHandsLeft;
    [SerializeField]
    private ListHands recHandsRight;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private string customGestureName;

    [SerializeField]
    private float timerOrig;
    private Node topNode;
    public HandFlag currentFlag = HandFlag.Left;
    public bool ckeckIN = false;
    public bool ckeckOUT = false;
    public bool ckeckTwo = false;
    public bool ckeckTwoJi = false;
    public int currentIndex;

    private void Awake()
    {
        leftHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
        rightHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
    }
    private void Start()
    {
        ConstructBehahaviourTree();
    }
    private void ConstructBehahaviourTree()
    {
        StartTrackNode startTrackNode = new StartTrackNode(this, leftHand, rightHand, customGestureName);
        SetWriteNode setWriteNode = new SetWriteNode(this, recHandsLeft, recHandsRight, customGestureName);
        Sequence mainSelector = new Sequence(new List<Node> { startTrackNode, setWriteNode });
        topNode = new Selector(new List<Node> { mainSelector });
    }
    void Update()
    {
        topNode.Evaluate();
        if (ckeckIN)
            ReadAllFiles(currentFlag);
        if(ckeckOUT)
            ShowVariants(currentFlag);
        if (ckeckTwo)
        {
            HideVariants(currentFlag, currentIndex);
            DrowTrajectory(currentFlag, currentIndex);
            ckeckTwo = false;
        }
        //if(checkTrajectory())
        //{
           
        //    Debug.Log("method");
        //}
    }

    public void ReadAllFiles(HandFlag handType)
    {
        switch (handType)
        { 
            case HandFlag.Left:
                IEnumerable<string> filesLeft = Directory.EnumerateFiles(Application.dataPath + "/Resources/Left/", "*.json");
                foreach (string filename in filesLeft)
                {
                    listHandsLeft.Add(filename);
                }
                ckeckIN = false;
                break; 
            case HandFlag.Right:
                IEnumerable<string> filesRight = Directory.EnumerateFiles(Application.dataPath + "/Resources/Right/", "*.json");
                foreach (string filename in filesRight)
                {
                    listHandsRight.Add(filename);
                }
                break;         
        }
    }
    public void ShowVariants(HandFlag handType)
    {
        switch (handType)
        {
            case HandFlag.Left:
                for (int i = 0; i < listHandsLeft.Count; i++)
                {
                    string filepath = listHandsLeft[i];
                    string handData = System.IO.File.ReadAllText(filepath);
                    recHandsLeft.listHands.Add(JsonUtility.FromJson<JointsCoordinats>(handData));
                    Vector3 position = new Vector3(recHandsLeft.listHands[i].jointsCoordinats[0].posX,
                        recHandsLeft.listHands[i].jointsCoordinats[0].posY,
                        recHandsLeft.listHands[i].jointsCoordinats[0].posZ);
                    Quaternion quaternion = Quaternion.Euler(recHandsLeft.listHands[i].jointsCoordinats[0].rotX,
                        recHandsLeft.listHands[i].jointsCoordinats[0].rotY,
                        recHandsLeft.listHands[i].jointsCoordinats[0].posZ);
                    GameObject handLeft = Instantiate(Resources.Load("WVRLeftHand", typeof(GameObject)), leftHand.transform) as GameObject;
                    handLeft.GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.2f);
                    handLeft.transform.localPosition = position;
                    handLeft.transform.localRotation = quaternion;

                }
                print("test array loaded ok");
                ckeckOUT = false;                
                break;
            case HandFlag.Right:
                for (int i = 0; i < listHandsRight.Count; i++)
                {
                    GameObject handRight = Instantiate(Resources.Load("WVRRightHand", typeof(GameObject)), rightHand.transform) as GameObject;
                    handRight.GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
                    string filepath = Application.dataPath + "/Resources/Right/" + listHandsLeft[0];
                    string handData = System.IO.File.ReadAllText(filepath);
                    recHandsLeft.listHands[i] = JsonUtility.FromJson<JointsCoordinats>(handData);
                    Vector3 position = new Vector3(recHandsRight.listHands[i].jointsCoordinats[0].posX,
                        recHandsRight.listHands[i].jointsCoordinats[0].posY,
                        recHandsRight.listHands[i].jointsCoordinats[0].posZ);
                    Quaternion quaternion = Quaternion.Euler(recHandsRight.listHands[i].jointsCoordinats[0].rotX,
                        recHandsRight.listHands[i].jointsCoordinats[0].rotY,
                        recHandsRight.listHands[i].jointsCoordinats[0].posZ);
                    handRight.transform.localPosition = position;
                    handRight.transform.localRotation = quaternion;
                    print("test array loaded ok");
                }
                break;
        }
    }
    public void HideVariants(HandFlag handType, int id)
    {
        switch (handType)
        {
            case HandFlag.Left:
                //GameObject handLeft = (GameObject)Resources.Load("WVRLeftHand", typeof(GameObject));
                GameObject[] exeptL = GameObject.FindGameObjectsWithTag("WVRLeftHand");
                for (int i = 0; i < exeptL.Length; i++)
                {
                    if (i != id)
                    {
                        Destroy(exeptL[i]);
                    }
                }
                break;
            case HandFlag.Right:
                //GameObject handRight = (GameObject)Resources.Load("WVRRightHand", typeof(GameObject));
                GameObject[] exeptR = GameObject.FindGameObjectsWithTag("WVRRightHand");
                for (int i = 0; i < exeptR.Length; i++)
                {
                    if (i != id)
                    {
                        Destroy(exeptR[i]);
                    }
                }
                break;
        }
    }
    public void DrowTrajectory(HandFlag handType, int id)
    {
        switch (handType)
        {
            case HandFlag.Left:
                lineRenderer.positionCount = (int)timerOrig;
                for (int i = 0; i < timerOrig; i++)
                {
                    lineRenderer.SetPosition(i, new Vector3(recHandsLeft.listHands[id].jointsCoordinats[i].posX,
                        recHandsLeft.listHands[id].jointsCoordinats[i].posY,
                        recHandsLeft.listHands[id].jointsCoordinats[i].posZ));
                    GameObject handPointLeft = Instantiate(Resources.Load("point", typeof(GameObject)), lineRenderer.transform) as GameObject;
                    handPointLeft.transform.localPosition = new Vector3(recHandsLeft.listHands[id].jointsCoordinats[i].posX,
                        recHandsLeft.listHands[id].jointsCoordinats[i].posY,
                        recHandsLeft.listHands[id].jointsCoordinats[i].posZ);
                    handPointLeft.GetComponent<checkPoints>().idPoint = i;
                }
                break;
            case HandFlag.Right:
                lineRenderer.positionCount = (int)timerOrig;
                for (int i = 0; i < timerOrig; i++)
                {
                    lineRenderer.SetPosition(i, new Vector3(recHandsRight.listHands[id].jointsCoordinats[i].posX,
                        recHandsRight.listHands[id].jointsCoordinats[i].posY,
                        recHandsRight.listHands[id].jointsCoordinats[i].posZ));
                    GameObject handPointRight = Instantiate(Resources.Load("point", typeof(GameObject)), lineRenderer.transform) as GameObject;
                    handPointRight.transform.localPosition = new Vector3(recHandsRight.listHands[id].jointsCoordinats[i].posX,
                        recHandsRight.listHands[id].jointsCoordinats[i].posY,
                        recHandsRight.listHands[id].jointsCoordinats[i].posZ);
                    handPointRight.GetComponent<checkPoints>().idPoint = i;
                }
                break;
        }
    }

    public bool checkTrajectory()
    {
        checkPoints[] listOfPoints = lineRenderer.GetComponentsInChildren<checkPoints>();
        bool passed = false;
        while(!passed)
        {
            for(int i = 0; i < listOfPoints.Length; i++)
            {
                if (listOfPoints[i].GetComponent<checkPoints>().pointPassed)
                    passed = true;
                else
                    passed = false;
            }
        }
        return passed;
    }

}
