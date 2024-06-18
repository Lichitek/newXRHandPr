using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIVE.OpenXR.Samples;
using Unity.XR;
using TMPro;
using System.Linq;
using System.IO;
using System;
using System.Reflection;

public class panelUI : MonoBehaviour
{
    public GameObject content;
    public TextMeshProUGUI nameMotion;
    public TextMeshProUGUI scriptMotion;
    public Image[] imageVariants;
    public GameObject panelPrefab;
    private TrackingHands trackingHands;
    private List<string> listLH = new List<string>();
    private List<string> listRH = new List<string>();

    private void Start()
    {
        trackingHands = this.GetComponent<TrackingHands>();
        IEnumerable<string> filesLeft = Directory.EnumerateFiles(Application.dataPath + "/Resources/Left/", "*.json");
        if (filesLeft.Count() != listLH.Count)
            foreach (string filename in filesLeft)
            {
                if (!listLH.Contains(filename))
                    listLH.Add(filename);
            }
        IEnumerable<string> filesRight = Directory.EnumerateFiles(Application.dataPath + "/Resources/Right/", "*.json");
        if (filesRight.Count() != listRH.Count)
            foreach (string filename in filesRight)
            {
                if (!listRH.Contains(filename))
                    listRH.Add(filename);
            }
        addContent();
    }

    public void addContent()
    {
        for(int i = 0; i < listLH.Count; i++)
        {
            nameMotion.text = nameMethod(HandFlag.Left, i);
            scriptMotion.text = trackingHands.listOfScriptsLeft[i];
            imageVariants[0].enabled = true;
            Instantiate(panelPrefab, content.transform);
        }
        for (int i = 0; i < listRH.Count; i++)
        {
            nameMotion.text = nameMethod(HandFlag.Right, i);
            scriptMotion.text = trackingHands.listOfScriptsRight[i];
            imageVariants[1].enabled = true;
            Instantiate(panelPrefab, content.transform);
        }
    }

    public string nameMethod(HandFlag handType, int index)
    {
        string subSring = "";
        string originalString = "";
        switch (handType)
        {
            case HandFlag.Left:
                subSring = Application.dataPath + "/Resources/Left/";
                originalString = listLH[index];
                break;
            case HandFlag.Right:
                subSring = Application.dataPath + "/Resources/Right/";
                originalString = listRH[index];
                break;
        }
        IEnumerable<string> set1 = originalString.Split('/').Distinct();
        IEnumerable<string> set2 = subSring.Split('/').Distinct();
        string toRemove = ".json";
        int i = set1.Except(set2).ToList()[0].IndexOf(toRemove);
        string methodName = set1.Except(set2).ToList()[0].Remove(i, toRemove.Length);
        return methodName;
    }


}
