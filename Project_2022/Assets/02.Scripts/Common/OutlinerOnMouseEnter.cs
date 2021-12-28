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
    [SerializeField] OutlineMode outlineMode;
    Outline outline = null;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = outlineWidth;
        outline.enabled = false;
    }

    public void OnPlayerMouseEnter()
    {
        if(outlineMode == OutlineMode.onlyEnterMouse)
        {
            outline.enabled = true;
            outline.OutlineWidth = outlineWidth;
        }
    }

    public void OnPlayerMouseExit()
    {
        outline.OutlineWidth = 0;
        outline.enabled = false;
    }

    public void OnGetPlayerMouse()
    {
        if (outlineMode == OutlineMode.onlyGetMouseInput)
        {
            outline.enabled = true;
            outline.OutlineWidth = outlineWidth;
        }
    }
}
