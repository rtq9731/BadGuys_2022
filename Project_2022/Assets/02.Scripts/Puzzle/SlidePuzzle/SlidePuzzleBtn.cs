using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidePuzzleBtn : MonoBehaviour
{
    [SerializeField]
    private SlidePuzzleManager manager;

    public void Onclick()
    {
        manager.PorceClear();
        this.enabled = false;
    }

    public void Selected(Color color)
    {
        GetComponent<Image>().color = color;

    }
}
