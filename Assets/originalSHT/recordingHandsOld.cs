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
public class FramesCoordinatsOld
{
    public string nameFile;
    public List<JointsCoordinatsOld> framesJoint = new List<JointsCoordinatsOld>(60);   
}

[System.Serializable]
public class JointsCoordinatsOld
{
    public List<CoordinatsOld> jointsCoord = new List<CoordinatsOld>(26);  
}

[System.Serializable]
public class CoordinatsOld
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

public class recordingHandsOld : MonoBehaviour
{
    [Header("Original file")]
    [SerializeField] public
    FramesCoordinatsOld frames = new FramesCoordinatsOld();

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
    FramesCoordinatsOld framesBeta = new FramesCoordinatsOld();

    void Start()
    {
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
        listUpdate();
        for (int i = 0; i < numFrames; i++)
        {
            frames.framesJoint.Add(new JointsCoordinatsOld());
            framesBeta.framesJoint.Add(new JointsCoordinatsOld());
        }
        for (int i = 0; i < numFrames; i++)
        {
            for (int j = 0; j < numJoints; j++)
            {
                frames.framesJoint[i].jointsCoord.Add(new CoordinatsOld());
                framesBeta.framesJoint[i].jointsCoord.Add(new CoordinatsOld());
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
        switch (debugHandGesture.startRecord[0].text)
        {
            case "Recording state":
                gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.yellow;
                StartCoroutine(enumRecording());
                break;
            case "OK":
                gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
                //StartCoroutine(enumPlaying());
                break;
            case "Like":
                gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.green;
                //StartCoroutine(enumPlaying());
                break;
        }

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
        FramesCoordinatsOld recHands = new FramesCoordinatsOld();
        for(int i = 0; i< methods.Count; i++)
        {
            string filepath = Application.dataPath + "/StreamingAssets/" + methods[i];
            string handData = System.IO.File.ReadAllText(filepath);
            recHands = JsonUtility.FromJson<FramesCoordinatsOld>(handData);
            print("test writeBetaList array loaded ok");
            playRecord(recHands);
        }
    }

    void playRecord(FramesCoordinatsOld method)
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
            if( i == 0 || i == (numFrames - 1))
            {
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
            }
            else
            {
                frames.framesJoint[i].jointsCoord[0].posX = HandTracking.GetHandJointLocations(HandOne)[0].position.x;
                frames.framesJoint[i].jointsCoord[0].posY = HandTracking.GetHandJointLocations(HandOne)[0].position.y;
                frames.framesJoint[i].jointsCoord[0].posZ = HandTracking.GetHandJointLocations(HandOne)[0].position.z;
                frames.framesJoint[i].jointsCoord[0].rotW = HandTracking.GetHandJointLocations(HandOne)[0].rotation.w;
                frames.framesJoint[i].jointsCoord[0].rotX = HandTracking.GetHandJointLocations(HandOne)[0].rotation.x;
                frames.framesJoint[i].jointsCoord[0].rotY = HandTracking.GetHandJointLocations(HandOne)[0].rotation.y;
                frames.framesJoint[i].jointsCoord[0].rotZ = HandTracking.GetHandJointLocations(HandOne)[0].rotation.z;
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