using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum OutlineMode
{
    onlyEnterMouse = 2,
    onlyGetMouseInput = 4,
    onlyScirpt = 6,
}

[RequireComponent(typeof(Outline))]
public class OutlinerOnMouseEnter : MonoBehaviour, IPlayerMouseEnterHandler, IPlayerMouseExitHandler, IGetPlayerMouseHandler
{
    [SerializeField] float outlineWidth = 10f;
    [SerializeField] OutlineMode outlineMode = OutlineMode.onlyEnterMouse;
    [SerializeField] Color outlineColor = new Color(100 / 255,100 / 255,100 / 255);
    Outline outline = null;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = outlineWidth;
        outline.OutlineColor = outlineColor;
        outline.enabled = false;
    }

    public void OnPlayerMouseEnter()
    {
        if(outlineMode == OutlineMode.onlyEnterMouse)
        {
            outline.enabled = true;
        }
    }

    public void OnPlayerMouseExit()
    {
        outline.enabled = false;
    }

    public void OnGetPlayerMouse()
    {
        if (outlineMode == OutlineMode.onlyGetMouseInput)
        {
            outline.enabled = true;
        }
    }
}
