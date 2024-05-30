using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIVE.OpenXR.Hand;
using VIVE.OpenXR.Toolkits.CustomGesture;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using static Unity.Burst.Intrinsics.X86;
using System;
using VIVE.OpenXR.Samples.Hand;
using VIVE.OpenXR.Samples;
using System.Xml.Linq;
using UnityEngine.XR;
using System.Linq;
using OpenCover.Framework.Model;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class cubeMan : MonoBehaviour
{
    Vector3 origin;

    public bool leftIsPlaying;
    public bool rightIsPlaying;
    public bool upIsPlaying;
    public bool downIsPlaying;
    

    void Start()
    {
        origin = transform.position;
        
    }

    private void FixedUpdate()
    {
        if (upIsPlaying)
        {
            moveUp();
            upIsPlaying=false;
        }
        if (downIsPlaying)
        {
            moveDown();
            downIsPlaying=false;

        }
        if (leftIsPlaying)
        {
            moveLeft();
            leftIsPlaying=false;
        }
        if (rightIsPlaying)
        {
            moveRight();
            rightIsPlaying=false;
        }
    }


    public void moveLeft()
    {
        transform.position += new Vector3(2, 0, 0);
    }
    public void moveRight()
    {
        transform.position += new Vector3(-2, 0, 0);
    }
    public void moveUp()
    {
        transform.position += new Vector3(0, 2, 0);
    }
    public void moveDown()
    {
        transform.position += new Vector3(0, -2, 0);
    }

    public void resetPos()
    {
        transform.position = origin;
    }
}
