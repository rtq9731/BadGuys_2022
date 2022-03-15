using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeBtn : MonoBehaviour, IInteractableItem
{
    public Color[] colors;
    public Color curColor;

    public List<Image> images = new List<Image>();

    int colorIndex;

    void Start()
    {
        curColor = colors[0];
    }

    public void Interact(GameObject taker)
    {
        if (colorIndex == colors.Length-1)
            colorIndex = -1;

        colorIndex++;
        curColor = colors[colorIndex];

        foreach (var item in images)
        {
            item.color = curColor;
        }

        Debug.Log(curColor);
    }

}
