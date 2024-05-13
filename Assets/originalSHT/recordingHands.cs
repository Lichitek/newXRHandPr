using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using static Unity.Burst.Intrinsics.X86;
using System;
using VIVE.OpenXR.Toolkits.CustomGesture;
using VIVE.OpenXR.Hand;
using VIVE.OpenXR.Samples.Hand;
using VIVE.OpenXR.Samples;
using System.Xml.Linq;
using UnityEngine.XR;
using System.Linq;
using OpenCover.Framework.Model;

[System.Serializable]
public class FramesCoordinats
{
    public string nameFile;
    public List<JointsCoordinats> framesJoint = new List<JointsCoordinats>(60);   
}

[System.Serializable]
public class JointsCoordinats
{
    public List<Coordinats> jointsCoord = new List<Coordinats>(26);  
}

[System.Serializable]
public class Coordinats
{
    //public Vector3 pos;
    public float posX;
    public float posY;
    public float posZ;
    public float rotW;
    public float rotX;
    public float rotY;
    public float rotZ;
}

public class recordingHands : MonoBehaviour
{
    [Header("Original file")]
    [SerializeField] public
    FramesCoordinats frames = new FramesCoordinats();

    [SerializeField] public string nameFile;


    public int numJoints = 26;
    public int numFrames = 3;

    public bool recordingNow = false;
    public bool playingNow = false;

    public HandFlag HandOne;

    [Header("Debug theme")]
    public DebugHandGesture debugHandGesture;
    List<string> methods = new List<string>();
    public List<string> methodsOfGameogject = new List<string>();
    public GameObject testGameobject;

    [SerializeField]
    public
    FramesCoordinats framesBeta = new FramesCoordinats();

    void Start()
    {
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
        listUpdate();
        for (int i = 0; i < numFrames; i++)
        {
            frames.framesJoint.Add(new JointsCoordinats());
            framesBeta.framesJoint.Add(new JointsCoordinats());
        }
        for (int i = 0; i < numFrames; i++)
        {
            for (int j = 0; j < numJoints; j++)
            {
                frames.framesJoint[i].jointsCoord.Add(new Coordinats());
                framesBeta.framesJoint[i].jointsCoord.Add(new Coordinats());
            }
        }
    }
    void listUpdate()
    {
        if (Directory.Exists(Application.dataPath + "/StreamingAssets"))
        {
            string worldsFolder = Application.dataPath + "/StreamingAssets";

            DirectoryInfo d = new DirectoryInfo(worldsFolder);
            foreach (var file in d.GetFiles("*.json"))
            {
                methods.Add(file.Name);
            }
        }
    }

    void Update()
    {

        if (debugHandGesture.startRecord[0].text == "Recording state")
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.yellow;
            recordingNow = true;
            if (debugHandGesture.stateRecL && recordingNow)
            {
                gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
                StartCoroutine(enumRecording());
            }                     
        }
        else
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
        if (debugHandGesture.startRecord[0].text == "Playing state")
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.black;
            playingNow = true;
            if (debugHandGesture.statePlayL && playingNow)
            {
                gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.green;
                //StartCoroutine(enumPlaying());
            }
        }
        else
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
    }
    public IEnumerator enumPlaying()
    {
        playingNow = false;
        int i = 0;

        while (i < numFrames)
        {
            int j = 0;
            while (j < numJoints)
            {
                framesBeta.framesJoint[i].jointsCoord[j].posX = HandTracking.GetHandJointLocations(HandOne)[j].position.x;
                framesBeta.framesJoint[i].jointsCoord[j].posY = HandTracking.GetHandJointLocations(HandOne)[j].position.y;
                framesBeta.framesJoint[i].jointsCoord[j].posZ = HandTracking.GetHandJointLocations(HandOne)[j].position.z;
                framesBeta.framesJoint[i].jointsCoord[j].rotW = HandTracking.GetHandJointLocations(HandOne)[j].rotation.w;
                framesBeta.framesJoint[i].jointsCoord[j].rotX = HandTracking.GetHandJointLocations(HandOne)[j].rotation.x;
                framesBeta.framesJoint[i].jointsCoord[j].rotY = HandTracking.GetHandJointLocations(HandOne)[j].rotation.y;
                framesBeta.framesJoint[i].jointsCoord[j].rotZ = HandTracking.GetHandJointLocations(HandOne)[j].rotation.z;
                j++;
            }
            yield return new WaitForSecondsRealtime(1f);
            Debug.Log("reltime = " + i);
            i++;
        }
        writeBetaList();
    }

    public void writeBetaList()
    {
        FramesCoordinats recHands = new FramesCoordinats();
        for(int i = 0; i< methods.Count; i++)
        {
            string filepath = Application.dataPath + "/StreamingAssets/" + methods[i];
            string handData = System.IO.File.ReadAllText(filepath);
            recHands = JsonUtility.FromJson<FramesCoordinats>(handData);
            print("test writeBetaList array loaded ok");
            playRecord(recHands);
        }
    }

    void playRecord(FramesCoordinats method)
    {
        if (framesBeta.framesJoint[0].jointsCoord[0].posX == method.framesJoint[0].jointsCoord[0].posX)
        {
            switch(method.nameFile)
            {
                case "moveLeft":
                    testGameobject.GetComponent<cubeMan>().moveLeft();
                    break;
                case "moveRight":
                    testGameobject.GetComponent<cubeMan>().moveRight();
                    break;
                case "moveUp":
                    testGameobject.GetComponent<cubeMan>().moveUp();
                    break;
                case "moveDown":
                    testGameobject.GetComponent<cubeMan>().moveDown();
                    break;
            }
        }
        
    }


    public IEnumerator enumRecording()
    {
        recordingNow = false;
        int i = 0;
        
        while (i < numFrames)
        {
            int j = 0;
            while (j < numJoints)
            {
                frames.framesJoint[i].jointsCoord[j].posX = HandTracking.GetHandJointLocations(HandOne)[j].position.x;
                frames.framesJoint[i].jointsCoord[j].posY = HandTracking.GetHandJointLocations(HandOne)[j].position.y;
                frames.framesJoint[i].jointsCoord[j].posZ = HandTracking.GetHandJointLocations(HandOne)[j].position.z;
                frames.framesJoint[i].jointsCoord[j].rotW = HandTracking.GetHandJointLocations(HandOne)[j].rotation.w;
                frames.framesJoint[i].jointsCoord[j].rotX = HandTracking.GetHandJointLocations(HandOne)[j].rotation.x;
                frames.framesJoint[i].jointsCoord[j].rotY = HandTracking.GetHandJointLocations(HandOne)[j].rotation.y;
                frames.framesJoint[i].jointsCoord[j].rotZ = HandTracking.GetHandJointLocations(HandOne)[j].rotation.z;
                j++;
            }
            yield return new WaitForSecondsRealtime(1f);
            Debug.Log("reltime = " + i);
            i++;
        }
        writeList();
    }

    public void writeList()
    {
        frames.nameFile = nameFile.ToString();
        string json = JsonUtility.ToJson(frames);
        string filepath = Application.dataPath + "/StreamingAssets/" + frames.nameFile.ToString() + ".json";
        Debug.Log(filepath);
        System.IO.File.WriteAllText(filepath, json);
        print("test writeList array saved ok");
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
        listUpdate();
    }
}