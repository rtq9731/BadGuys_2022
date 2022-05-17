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
        rect = guidePanel.GetComponent<RectTransform>();
        originPos = rect.anchoredPosition;
    }
    void Update()
    {
        if (Input.anyKeyDown && isFirstInput)
        {
            FirstGuide();
            isOnPanel = true;
        }
        
        if(isOnPanel)
        {
            timer += Time.deltaTime;
            if(timer >= 7f)
            {
                isOnPanel = false;
                timer = 0;
            }
        }
    }

    void FirstGuide()
    {
        guidePanel.gameObject.SetActive(true);
    }

}
