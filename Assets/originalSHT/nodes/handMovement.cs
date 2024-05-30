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

public class handMovement : Node
{
    private CustomGestureManager HGM;
    private CustomGestureDefiner GD;
    private CustomGesture _Gestures;

    public handMovement(CustomGestureManager hGM, CustomGestureDefiner gD, CustomGesture cG)
    {
        this.HGM = hGM;
        this.GD = gD;
        this._Gestures = cG;
    }

    public override NodeState Evaluate()
    {
        return NodeState.PLAY;
    }
            
}
