using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private Text tooltipTxt;

    [SerializeField] private string[] tooltip;

    private int tooltipIndex;
    
    private void Start()
    {
        tooltipIndex = Random.Range(0, tooltip.Length);

        tooltipTxt.text = tooltip[tooltipIndex];
    }
}
