using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

[RequireComponent(typeof(ContentSizeFitter))]
public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance = null;

    public int padding = 0;
    public int limitPanelCount = 0;

    public ShowInventoryUI showInventoryUI;

    public DialogPanel panelPrefab = null;
    List<DialogPanel> dialogs = new List<DialogPanel>();

    Stack<DialogData> lastDialogs = new Stack<DialogData>();

    RectTransform myRect = null;

    Coroutine cor = null;

    Vector3 originRect;

    int curOrder = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        myRect = GetComponent<RectTransform>();
        
        originRect = myRect.anchoredPosition;
        Debug.Log(myRect.rect.y);
        Debug.Log(originRect);
        GameManager.Instance._onPauseChanged += OnGamePause;
    }

    private void Start()
    {
        showInventoryUI = FindObjectOfType<ShowInventoryUI>();
    }

    void OnGamePause(bool isPause)
    {
        if (!Application.isPlaying)
            return;

        if(isPause)
        {
            if(cor != null)
            {
                StopCoroutine(cor);
            }
        }
        else
        {
            if (cor == null && gameObject.activeSelf)
            {
                cor = StartCoroutine(PlayDialog());
            }
        }
    }

    private void Update()
    {
        SetPositionPanels();
    }



    public void ClearALLDialog()
    {
        lastDialogs.Clear();
        dialogs.ForEach(x => x.gameObject.SetActive(false));
    }

    private void CreateDialogPanel(string name, string text, Color color)
    {
        gameObject.SetActive(true);
        DialogPanel dialogPanel = dialogs.Find(x => !x.gameObject.activeSelf);
        if(dialogPanel == null)
        {
            if(dialogs.Count <= limitPanelCount + 1)
            {
                dialogPanel = Instantiate<DialogPanel>(panelPrefab, transform);
                dialogs.Add(dialogPanel);
            }
        }
        dialogPanel.order = curOrder;   
        curOrder++;

        if (dialogs.Count >= limitPanelCount + 1)
        {
            dialogs.Sort((x, y) => -x.order.CompareTo(y.order));
            dialogs[0].SetActiveFalseImmediately();
        }

        dialogPanel.SetActive(true, color, name + " : " + text, CheckDialog);
    }

    private void SetPositionPanels()
    {
        dialogs.Sort((x, y) => -x.order.CompareTo(y.order));
        float height = 0f;
        //Debug.Log(height);
        for (int i = 0; i < dialogs.Count; i++)
        {
            dialogs[i].rectTrm.anchoredPosition = new Vector2(0, height);
            height += dialogs[i].rectTrm.rect.height;
        }
        myRect.sizeDelta = new Vector2(panelPrefab.rectTrm.rect.width, height);
    }

    public void CheckDialog()
    {
        if(dialogs.Find(x => x.gameObject.activeSelf) == null)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetDialaogs(List<DialogData> datas)
    {
        foreach (var item in datas)
        {
            lastDialogs.Push(item);
        }

        if (cor == null)
        {
            cor = StartCoroutine(PlayDialog());
        }
    }

    IEnumerator PlayDialog()
    {
        while (lastDialogs.Count > 0)
        {
            DialogData data = lastDialogs.Pop();
            CreateDialogPanel(data.name, data.str, data.color);
            yield return new WaitForSeconds(3f);
        }
    }

    public void SetDialogPos(bool isInventoryDown)
    {
        if(isInventoryDown)
        {
            myRect.DOAnchorPos(originRect, 0.5f);
            Debug.Log("내려간다~");
        }
        else
        {
            myRect.DOAnchorPos(new Vector2(myRect.anchoredPosition.x , originRect.y + 180f), 0.5f);
            Debug.Log("올라간다~");
        }
    }
}

