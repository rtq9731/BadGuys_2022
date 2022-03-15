using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ShowInventoryUI : MonoBehaviour
{
    public float fadeTime;

    private RectTransform rectTrm;
    public Vector3 originRectTrm;
    Dictionary<KeyCode, int> m_keyCallFunc;

    public bool isInventoryDown = false;

    private void Start()
    {
        rectTrm = GetComponent<RectTransform>(); 
        
        originRectTrm = rectTrm.anchoredPosition;

        m_keyCallFunc = new Dictionary<KeyCode, int>()
        {
             {KeyCode.Alpha1, 1},
             {KeyCode.Alpha2, 2},
             {KeyCode.Alpha3, 3},
             {KeyCode.Alpha4, 4},
             {KeyCode.Alpha5, 5},
             {KeyCode.Alpha6, 6},
             {KeyCode.Alpha7, 7},
             {KeyCode.Alpha8, 8},
             {KeyCode.Alpha9, 9},
             {KeyCode.E, 10}

        };

        isInventoryDown = true;
        rectTrm.DOAnchorPos(new Vector3(rectTrm.transform.position.x, -100), 0.5f);
    }

    private void Update()
    {
        DownInventorySlot();
        fadeTime += Time.deltaTime;

        if (Input.anyKeyDown)
        {
            foreach (var dic in m_keyCallFunc)
            {
                if (Input.GetKeyDown(dic.Key) )
                {
                    fadeTime = 0;
                    ShowInventorySlot();
                }
            }
        }

        if(Input.mouseScrollDelta.y != 0)
        {
            fadeTime = 0;
            ShowInventorySlot();
        }
    }

    public void ShowInventorySlot()
    {
        if(transform.childCount >= 0)
        {
            isInventoryDown = false;
            rectTrm.DOAnchorPos(originRectTrm, 0.5f);
            DialogManager.Instance.SetDialogPos(isInventoryDown);
        }
    }

    public void DownInventorySlot()
    {
        if (fadeTime >= 4f && !isInventoryDown)
        {
            isInventoryDown = true;
            rectTrm.DOAnchorPos(new Vector3(rectTrm.anchoredPosition.x, -100), 0.5f);
            DialogManager.Instance.SetDialogPos(isInventoryDown);
        }
    }
}
