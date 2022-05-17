using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIInteractHelpPanelManager : MonoBehaviour
{
    public static UIInteractHelpPanelManager Instance
    { 
        get
        {
            return _instance;
        }
    }

    private static UIInteractHelpPanelManager _instance = null;

    [SerializeField] RectTransform helpPanelPrefab = null;

    Queue<RectTransform> helpPanelPool = new Queue<RectTransform>();

    private void Awake()
    {
        _instance = this;
    }

    public void ShowHelpPanel(string msg)
    {
        RectTransform curPanel = null;

        if (helpPanelPool.Count < 1)
        {
            curPanel = MakeNewPanel();
        }
        else
        {
            if(helpPanelPool.Peek().gameObject.activeSelf)
            {
                curPanel = MakeNewPanel();
            }
            else
            {
                curPanel = helpPanelPool.Dequeue();
                helpPanelPool.Enqueue(curPanel);
            }
        }

        curPanel.gameObject.SetActive(true);

        Text panelText = curPanel.GetComponentInChildren<Text>();

        panelText.color = new Color(panelText.color.r, panelText.color.g, panelText.color.b, 0);
        panelText.text = msg;

        curPanel.DOAnchorPos(new Vector2(0, 0), 2f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            panelText.DOFade(1f, 2f).OnComplete(() =>
            {
                panelText.DOFade(0, 2f).OnComplete(() =>
                {
                    curPanel.DOAnchorPos(new Vector2(0, -1024f), 2f).SetEase(Ease.OutBack).OnComplete(() => curPanel.gameObject.SetActive(false));
                });
            });
        });
    }

    private RectTransform MakeNewPanel()
    {
        RectTransform curPanel = Instantiate(helpPanelPrefab, transform);

        helpPanelPool.Enqueue(curPanel);
        curPanel.gameObject.SetActive(false);

        return curPanel;
    }
}
