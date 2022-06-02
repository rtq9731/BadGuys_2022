using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Triggers;

public class GuidePanelManager : MonoBehaviour
{
    public List<Guides> guides = new List<Guides>();

    int guideIndex = 0;

    GuidePanel guide;
    void Start()
    {
        guide = GetComponent<GuidePanel>();
    }

    public void ClearGuide()
    {
        guideIndex++;
    }

    [System.Serializable]
    public class Guides
    {
        public bool isComplate;
        public string guideStr;
    }
}
