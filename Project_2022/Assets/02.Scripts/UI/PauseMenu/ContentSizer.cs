using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ContentSizer : MonoBehaviour
{
    Padding padding;
    float spacing = 0f;

    [SerializeField] List<RectTransform> panels = null;

    Vector2 newSize = Vector2.zero;
    HorizontalLayoutGroup layoutGroup = null;

    RectTransform rect = null;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        layoutGroup = GetComponent<HorizontalLayoutGroup>();

        padding = new Padding(layoutGroup.padding);
        spacing = layoutGroup.spacing;
    }

    private void OnGUI()
    {
        UpdateSize();
    }

    private void UpdateSize()
    {
        newSize = Vector2.zero;

        float bigHeight = 0f;

        List<RectTransform> ls = panels.FindAll(item => item.gameObject.activeSelf);
        for (int i = 0; i < ls.Count; i++)
        {
            Debug.Log(ls[i].rect.width * ls[i].lossyScale.x);

            if (i == 0)
            {
                newSize.x += padding.left;
            }

            newSize.x += ls[i].rect.width * ls[i].lossyScale.x + spacing;
            
            if(ls[i].rect.height > bigHeight)
            {
                bigHeight = ls[i].rect.height;
            }

            if (i == ls.Count - 1)
            {
                newSize.x += padding.right;
            }
        }

        newSize.y = bigHeight;

        rect.sizeDelta = newSize;
        newSize = Vector2.zero;
    }
}

[System.Serializable]
class Padding
{
    public Padding(RectOffset padding)
    {
        left = padding.left;
        right = padding.right;
        top = padding.top;
        bottom = padding.bottom;
    }

    public float left;
    public float right;
    public float top;
    public float bottom;
}

