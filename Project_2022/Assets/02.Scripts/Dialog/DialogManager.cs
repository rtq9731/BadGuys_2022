using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance
    {
        get { return _instance; }
    }

    private static DialogManager _instance;

    [SerializeField] DialogPanel dialogPanelPrefab;

    [SerializeField] int maxDialogCount = 2;
    [SerializeField] float textTime = 0.1f;
    [SerializeField] float waitTime = 1f;

    bool isNeedToWait = false;

    int curOrder = 0;

    RectTransform myRect = null;

    Vector2 originRect = Vector2.zero;

    List<DialogPanel> dialogPanelPool = new List<DialogPanel>();
    Queue<Stack<char>> dialogDatas = new Queue<Stack<char>>();

    Stack<char> curDialog = new Stack<char>();
    
    DialogPanel curDialogPanel = null;

    Coroutine cor = null;

    bool isFirst = true;

    float waitTimer = 0f;

    private void Awake()
    {
        _instance = this;

        myRect = GetComponent<RectTransform>();
        originRect = myRect.anchoredPosition;
    }

    public void OnEnable()
    {
        if(isFirst)
        {
            isFirst = false;
            return;
        }

        if(dialogDatas.Count > 0 || curDialog.Count > 0)
        {
            if (cor != null)
            {
                StopCoroutine(cor);
            }

            cor = StartCoroutine(PlayDialog());
        }
    }

    public void SetDialogData(List<DialogData> datas)
    {
        gameObject.SetActive(true);

        datas.Sort((x, y) => x.id.CompareTo(y.id));

        foreach (var item in datas)
        {
            dialogDatas.Enqueue(DialogDataToStack(item)); // 다이얼로그 선입선출 구조
            UIManager.Instance.SetStopMenuDialog(item.name + " : " + item.str, item.color);
        }


        //foreach (var item in datas)
        //{
        //    UIManager.Instance.SetStopMenuDialog($"{item.name} : {item.str}", item.color);
        //}

        if (cor != null)
        {
            StopCoroutine(cor);
        }

        cor = StartCoroutine(PlayDialog());
    }

    private Stack<char> DialogDataToStack(DialogData data)
    {
        Stack<char> result = new Stack<char>();

        for (int i = data.str.Length - 1; i >= 0; i--)
        {
            result.Push(data.str[i]);
        }

        result.Push(' ');
        result.Push(':');
        result.Push(' ');

        for (int i = data.name.Length - 1; i >= 0; i--)
        {
            result.Push(data.name[i]);
        }

        string colorString = ColorUtility.ToHtmlStringRGBA(data.color);
        result.Push('/');
        for (int i = colorString.Length - 1; i >= 0; i--)
        {
            result.Push(colorString[i]);
        }

        result.Push('#');

        return result;
    }

    private DialogPanel GetDialogPanel()
    {
        DialogPanel result = dialogPanelPool.Find(x => !x.gameObject.activeSelf);

        if (result == null)
        {
            if (dialogPanelPool.Count <= maxDialogCount + 1)
            {
                result = Instantiate<DialogPanel>(dialogPanelPrefab, transform);
                dialogPanelPool.Add(result);
            }
        }

        result.order = curOrder;
        curOrder++;
        result.transform.SetAsLastSibling();

        if (dialogPanelPool.Count >= maxDialogCount + 1)
        {
            dialogPanelPool.Sort((x, y) => -x.order.CompareTo(y.order));
            dialogPanelPool[0].RemoveNow();
        }

        result.gameObject.SetActive(true);

        return result;
    }

    public void ClearALLDialog()
    {
        while (dialogDatas.Count > 0)
        {
            dialogDatas.Dequeue().Clear();
        }
        dialogDatas.Clear();

        dialogPanelPool.ForEach(x => x.RemoveNow());
    }

    IEnumerator PlayDialog()
    {
        if(isNeedToWait)
        {
            while (waitTimer < waitTime)
            {
                if (!GameManager.Instance.IsPause)
                {
                    waitTimer += Time.deltaTime;
                }
                yield return null;
            }
            isNeedToWait = false;
            waitTimer = 0f;
        }

        while (dialogDatas.Count > 0 || curDialog != null)
        {
            if(curDialogPanel == null)
            {
                curDialogPanel = GetDialogPanel();
            }

            if(!curDialogPanel.isInited)
            {
                if (curDialog.Count < 1)
                {
                    if (dialogDatas.Count < 1)
                    {
                        yield break;
                    }

                    curDialog = dialogDatas.Dequeue();
                }

                string colorString = "";

                while (curDialog.Peek() != '/')
                {
                    colorString += curDialog.Pop();
                }
                curDialog.Pop();

                Color color = Color.red;
                ColorUtility.TryParseHtmlString(colorString, out color);

                curDialogPanel.SetTextColor(color);
                curDialogPanel.InitPanel();

                yield return new WaitUntil(curDialogPanel.IsOn);
            } // 지금 진행중인 패널이 있을 때
            else
            {
                if(!curDialogPanel.IsOn())
                    yield return new WaitUntil(curDialogPanel.IsOn);
            }

            while (curDialog.Count > 0) // 글자 재생 반복문
            {
                yield return new WaitForSeconds(textTime);

                curDialogPanel.SetText(curDialog.Pop());
            }

            isNeedToWait = true;
            while (waitTimer < waitTime)
            {
                if(!GameManager.Instance.IsPause)
                {
                    waitTimer += Time.deltaTime;
                }
                yield return null;
            }
            isNeedToWait = false;
            waitTimer = 0f;

            // 다이얼로그 끝!
            curDialogPanel.Remove();
            curDialogPanel = null;


            yield return null;
        }
        yield return null;
    }
    public void SetDialogPos(bool isInventoryDown)
    {
        if (isInventoryDown)
        {
            myRect.DOAnchorPos(originRect, 0.5f);
        }
        else
        {
            myRect.DOAnchorPos(new Vector2(myRect.anchoredPosition.x, originRect.y + 180f), 0.5f);
        }
    }
}
