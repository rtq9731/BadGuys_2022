using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidePanelManager : MonoBehaviour
{
    public List<Guides> guides = new List<Guides>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    [System.Serializable]
    public class Guides
    {
        public bool isComplate;
        public string guideStr;
    }
}
