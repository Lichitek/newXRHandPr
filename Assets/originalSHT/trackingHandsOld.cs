using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using VIVE.OpenXR.Samples;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using static Unity.Burst.Intrinsics.X86;
using System;
using VIVE.OpenXR.Toolkits.CustomGesture;
using VIVE.OpenXR.Hand;
using VIVE.OpenXR.Samples.Hand;
using System.Xml.Linq;
using System.Linq;
public class trackingHandsOld : MonoBehaviour
{
//    public GameObject handBones;
//    [SerializeField]
//    recordingHandsOld recHands = new recordingHandsOld();

//    public string name;
//    public bool str = false;
//    List<GameObject> _Temp = new List<GameObject>();
//    public int _tempIndex = 0;
//    public int numJoints = 26;
//    void Start()
//    {
              
//    }


//    void Update()
//    {
//        if (str)
//        {
//            _tempIndex = 0;
//            readList();
//            addJoints(handBones);
//            _Temp.Insert(1, handBones);
//            /*Debug.Log(_Temp.Count);
//            for (int i = 0; i < _Temp.Count; i++)
//            {
//                Debug.Log(_Temp[i].name);                
//            }*/
//            setPosition(handBones);

//            str = false;
//        }

//    }

//    public void readList()
//    {
//        string filepath = Application.dataPath + "/StreamingAssets/" + name + ".json";
//        string handData = System.IO.File.ReadAllText(filepath);
//        recHands.frames = JsonUtility.FromJson<FramesCoordinatsOld>(handData);
//        print("test array loaded ok");
//    }

//    void addJoints(GameObject _GObj)
//    {
//        for (int i = 0; i < _GObj.transform.childCount; i++)
//        {          
//            _Temp.Add(_GObj.transform.GetChild(i).gameObject);
//            Debug.Log(_GObj.transform.GetChild(i).gameObject.name);
//            addJoints(_GObj.transform.GetChild(i).gameObject);            
//        }

//    }
//    void setPosition(GameObject _GObj)
//    {
//        Debug.Log("set = " + _tempIndex);
//        while (_tempIndex < numJoints)
//        {
//            if (_tempIndex == 1)
//            {
//                _GObj.transform.localPosition = new Vector3(recHands.frames.framesJoint[0].jointsCoord[1].posX,
//                    recHands.frames.framesJoint[0].jointsCoord[1].posY,
//                    recHands.frames.framesJoint[0].jointsCoord[1].posZ);
//                _GObj.transform.localRotation = new Quaternion(recHands.frames.framesJoint[0].jointsCoord[1].rotW,
//                    recHands.frames.framesJoint[0].jointsCoord[1].rotX,
//                    recHands.frames.framesJoint[0].jointsCoord[1].rotY,
//                    recHands.frames.framesJoint[0].jointsCoord[1].rotZ);
//                _tempIndex++;
//            }


//            for (int i = 0; i < _GObj.transform.childCount; i++)
//            {
//                _GObj.transform.GetChild(i).gameObject.transform.localPosition = new Vector3(recHands.frames.framesJoint[0].jointsCoord[_tempIndex].posX,
//                    recHands.frames.framesJoint[0].jointsCoord[_tempIndex].posY,
//                    recHands.frames.framesJoint[0].jointsCoord[_tempIndex].posZ);
//                _GObj.transform.GetChild(i).gameObject.transform.localRotation = new Quaternion(recHands.frames.framesJoint[0].jointsCoord[_tempIndex].rotW,
//                    recHands.frames.framesJoint[0].jointsCoord[_tempIndex].rotX,
//                    recHands.frames.framesJoint[0].jointsCoord[_tempIndex].rotY,
//                    recHands.frames.framesJoint[0].jointsCoord[_tempIndex].rotZ);
//                _tempIndex++;
//                setPosition(_GObj.transform.GetChild(i).gameObject);
//            }
//        }

    //}
}
