using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GuidePanel : MonoBehaviour
{
    RectTransform rect;

    Vector3 originPos;

    bool isFirstInput;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        originPos = rect.anchoredPosition;
    }
    void Update()
    {
        if (Input.anyKeyDown && isFirstInput)
        {
            ShowGuidePanel();
        }
    }

    void ShowGuidePanel()
    {
        if(isFirstInput)
        {
            rect.DOAnchorPos(new Vector3(50, -50, 0), 0.5f);
            isFirstInput = false;
        }
    }

    void HideGuidePanel()
    {
        rect.DOAnchorPos(originPos, 0.5f);
    }
}
