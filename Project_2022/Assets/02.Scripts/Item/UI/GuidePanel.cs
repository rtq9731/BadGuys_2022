using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GuidePanel : MonoBehaviour
{
    [SerializeField] GameObject guidePanel;

    public Text guideText;

    GuidePanelManager guidePanelManager;

    RectTransform rect;

    Vector3 originPos;

    float timer;

    bool isFirstInput = true;
    bool isOnPanel = false;
    private void Start()
    {
        guidePanelManager = GetComponent<GuidePanelManager>();
        rect = guidePanel.GetComponent<RectTransform>();
        originPos = rect.anchoredPosition;
    }
    void Update()
    {
        if (Input.anyKeyDown && isFirstInput)
        {
            ShowGuidePanel();
            isOnPanel = true;
        }
        
        if(isOnPanel)
        {
            timer += Time.deltaTime;
            if(timer >= 7f)
            {
                HideGuidePanel();
                isOnPanel = false;
                timer = 0;
            }
        }
    }

    void ShowGuidePanel()
    {
        if(isFirstInput)
        {
            rect.DOAnchorPos(new Vector3(130, -50, 0), 0.5f);
            isFirstInput = false;
        }
    }

    public void HideGuidePanel()
    {
        rect.DOAnchorPos(originPos, 0.5f).OnComplete(() =>
        {
            guidePanelManager.ClearGuide();
            guidePanelManager.SetText();
            isFirstInput = true;
        });
    }
}
