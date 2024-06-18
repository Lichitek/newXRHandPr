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
using static RecordingHands;
using System.Linq;
using OpenCover.Framework.Model;
using static Unity.VisualScripting.Member;

public class TrackingHands : MonoBehaviour
{
    [SerializeField]
    public List<string> listHandsLeft = new List<string>();
    [SerializeField]
    public List<string> listHandsRight = new List<string>();
    [SerializeField]
    public ListHands recHandsLeft;
    [SerializeField]
    public ListHands recHandsRight;
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private LineRenderer lineRendererLeft;
    [SerializeField]
    private LineRenderer lineRendererRight;
    [SerializeField]
    private string customGestureNameStart;
    [SerializeField]
    private string customGestureNameEnd;
    [SerializeField]
    private float timerOrig;

    private Node topNode;
    public HandFlag currentFlag;
    public int currentIndex;
    [SerializeField]
    public List<string> listOfScriptsLeft = new List<string>();
    public List<string> listOfScriptsRight = new List<string>();
    public List<GameObject> objectsScriptsLeft = new List<GameObject>();
    public List<GameObject> objectsScriptsRight = new List<GameObject>();
    public bool ckeckShow = false;
    public bool ckeckVariant = false;

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
        StartTrackNode startTrackNode = new StartTrackNode(this, listHandsLeft, listHandsRight, leftHand, rightHand, customGestureNameStart);
        SetTrackNode setWriteNode = new SetTrackNode(this, currentFlag, recHandsLeft, recHandsRight, customGestureNameStart);
        ContinueTrackNode continueTrackNode = new ContinueTrackNode(this, lineRendererLeft, lineRendererRight, customGestureNameStart);
        EndTrackNode endTrackNode = new EndTrackNode(this, leftHand, rightHand, customGestureNameEnd);
        Sequence mainSelector = new Sequence(new List<Node> { startTrackNode, setWriteNode, continueTrackNode, endTrackNode });
        topNode = new Selector(new List<Node> { mainSelector });
    }
    void Update()
    {
        topNode.Evaluate();
    }

    //public void ReadAllFiles(HandFlag handType)
    //{
    //    switch (handType)
    //    { 
    //        case HandFlag.Left:
    //            IEnumerable<string> filesLeft = Directory.EnumerateFiles(Application.dataPath + "/Resources/Left/", "*.json");
    //            if(filesLeft.Count() > listHandsLeft.Count)
    //                foreach (string filename in filesLeft)
    //                {
    //                    listHandsLeft.Add(filename);
    //                }
    //            break; 
    //        case HandFlag.Right:
    //            IEnumerable<string> filesRight = Directory.EnumerateFiles(Application.dataPath + "/Resources/Right/", "*.json");
    //            if (filesRight.Count() > listHandsRight.Count)
    //                foreach (string filename in filesRight)
    //                {
    //                    listHandsRight.Add(filename);
    //                }
    //            break;         
    //    }
    //}
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
                ckeckShow = true;
                break;
            case HandFlag.Right:
                for (int i = 0; i < listHandsRight.Count; i++)
                {
                    string filepath = listHandsRight[i];
                    string handData = System.IO.File.ReadAllText(filepath);
                    recHandsRight.listHands[i] = JsonUtility.FromJson<JointsCoordinats>(handData);
                    Vector3 position = new Vector3(recHandsRight.listHands[i].jointsCoordinats[0].posX,
                        recHandsRight.listHands[i].jointsCoordinats[0].posY,
                        recHandsRight.listHands[i].jointsCoordinats[0].posZ);
                    Quaternion quaternion = Quaternion.Euler(recHandsRight.listHands[i].jointsCoordinats[0].rotX,
                        recHandsRight.listHands[i].jointsCoordinats[0].rotY,
                        recHandsRight.listHands[i].jointsCoordinats[0].posZ);
                    GameObject handRight = Instantiate(Resources.Load("WVRRightHand", typeof(GameObject)), rightHand.transform) as GameObject;
                    handRight.transform.localPosition = position;
                    handRight.transform.localRotation = quaternion;
                }
                ckeckShow = true;
                break;
        }
    }
    public void HideVariants(HandFlag handType, int id)
    {
        switch (handType)
        {
            case HandFlag.Left:
                GameObject[] exeptL = GameObject.FindGameObjectsWithTag("WVRLeftHand");
                for (int i = 0; i < exeptL.Length; i++)
                {
                    if (i != id)
                        Destroy(exeptL[i]);
                }
                break;
            case HandFlag.Right:
                GameObject[] exeptR = GameObject.FindGameObjectsWithTag("WVRRightHand");
                for (int i = 0; i < exeptR.Length; i++)
                {
                    if (i != id)
                        Destroy(exeptR[i]);
                }
                break;
        }
    }
    public void DrowTrajectory(HandFlag handType, int id)
    {
        switch (handType)
        {
            case HandFlag.Left:
                lineRendererLeft.positionCount = (int)timerOrig;
                for (int i = 0; i < timerOrig; i++)
                {
                    lineRendererLeft.SetPosition(i, new Vector3(recHandsLeft.listHands[id].jointsCoordinats[i].posX,
                        recHandsLeft.listHands[id].jointsCoordinats[i].posY,
                        recHandsLeft.listHands[id].jointsCoordinats[i].posZ));
                    GameObject handPointLeft = Instantiate(Resources.Load("point", typeof(GameObject)), lineRendererLeft.transform) as GameObject;
                    handPointLeft.transform.localPosition = new Vector3(recHandsLeft.listHands[id].jointsCoordinats[i].posX,
                        recHandsLeft.listHands[id].jointsCoordinats[i].posY,
                        recHandsLeft.listHands[id].jointsCoordinats[i].posZ);
                    handPointLeft.GetComponent<CkeckPoints>().idPoint = i;
                }
                break;
            case HandFlag.Right:
                lineRendererRight.positionCount = (int)timerOrig;
                for (int i = 0; i < timerOrig; i++)
                {
                    lineRendererRight.SetPosition(i, new Vector3(recHandsRight.listHands[id].jointsCoordinats[i].posX,
                        recHandsRight.listHands[id].jointsCoordinats[i].posY,
                        recHandsRight.listHands[id].jointsCoordinats[i].posZ));
                    GameObject handPointRight = Instantiate(Resources.Load("point", typeof(GameObject)), lineRendererRight.transform) as GameObject;
                    handPointRight.transform.localPosition = new Vector3(recHandsRight.listHands[id].jointsCoordinats[i].posX,
                        recHandsRight.listHands[id].jointsCoordinats[i].posY,
                        recHandsRight.listHands[id].jointsCoordinats[i].posZ);
                    handPointRight.GetComponent<CkeckPoints>().idPoint = i;
                }
                break;
        }
    }

    public void PlayMethod(HandFlag handType, int index)
    {
        string subSring = "";
        string originalString = "";
        IEnumerable<string> set1;
        IEnumerable<string> set2;
        string toRemove;
        int i;
        string methodName;
        Type thisType;
        MethodInfo theMethod;
        switch (handType)
        { 
            case HandFlag.Left:
                subSring = Application.dataPath + "/Resources/Left/";
                originalString = listHandsLeft[index];
                set1 = originalString.Split('/').Distinct();
                set2 = subSring.Split('/').Distinct();
                toRemove = ".json";
                i = set1.Except(set2).ToList()[0].IndexOf(toRemove);
                methodName = set1.Except(set2).ToList()[0].Remove(i, toRemove.Length);
                thisType = objectsScriptsLeft[index].GetComponent(listOfScriptsLeft[index]).GetType();
                theMethod = thisType.GetMethod(methodName);
                theMethod.Invoke(objectsScriptsLeft[index].GetComponent(listOfScriptsLeft[index]), null);
                break; 
            case HandFlag.Right:
                subSring = Application.dataPath + "/Resources/Right/";
                originalString = listHandsRight[index];
                set1 = originalString.Split('/').Distinct();
                set2 = subSring.Split('/').Distinct();
                toRemove = ".json";
                i = set1.Except(set2).ToList()[0].IndexOf(toRemove);
                methodName = set1.Except(set2).ToList()[0].Remove(i, toRemove.Length);
                thisType = objectsScriptsRight[index].GetComponent(listOfScriptsRight[index]).GetType();
                theMethod = thisType.GetMethod(methodName);
                theMethod.Invoke(objectsScriptsRight[index].GetComponent(listOfScriptsRight[index]), null);
                break;
        }
    }

}
