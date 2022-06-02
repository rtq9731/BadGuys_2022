using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopMenuDialogPanel : MonoBehaviour
{
    [SerializeField] Transform dialogParent;
    [SerializeField] Text textPrefab = null;

    public void SetDialog(string str, Color color)
    {
        Text curText = Instantiate<Text>(textPrefab, dialogParent);
        curText.text = str;
        curText.color = color;
    }
}
