using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using System;
using VIVE.OpenXR.Toolkits.CustomGesture;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class recordingHands : MonoBehaviour
{
    public class JointsCoordinats
    {
        public List<Coordinats> jointsCoord = new List<Coordinats>(30);
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
    private JointsCoordinats frames = new JointsCoordinats();
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private string fileName;
    [SerializeField]
    private float timer;

    [SerializeField] private CustomGestureManager HGM;
    [SerializeField] private CustomGestureDefiner GD;
    [SerializeField] private LineRenderer lineRenderer;

    private Node topNode;

    private void Awake()
    {
        GD = FindAnyObjectByType<CustomGestureDefiner>();
        HGM = FindAnyObjectByType<CustomGestureManager>();
        leftHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
        rightHand.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
        for (int i = 0; i < ((int)timer); i++)
        {
            frames.jointsCoord.Add(new Coordinats());
        }
    }

    private void Start()
    {
        ConstructBehahaviourTree();
    }
    private void ConstructBehahaviourTree()
    {
        startWriteNode startWriteNode = new startWriteNode(GD.DefinedGestures, leftHand, rightHand);
        continueWriteNode continueWriteNode = new continueWriteNode(frames, GD.DefinedGestures, lineRenderer, timer, fileName);
        endWriteNode endWriteNode = new endWriteNode(GD.DefinedGestures, leftHand, rightHand);

        Selector mainSelector = new Selector(new List<Node> { startWriteNode, continueWriteNode, endWriteNode });

        topNode = new Selector(new List<Node> { mainSelector });
    }

    private void Update()
    {
        topNode.Evaluate();
    }

}
