using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GuidePanel : MonoBehaviour
{
    [SerializeField] GameObject guidePanel;
    [SerializeField] GameObject[] guideDetail;

    [SerializeField] Text falseImage;

    [SerializeField] GameObject guideDetailParent;

    bool isFirstInput = true;
    bool isOnPanel = false;
    bool isTrigger = true;

    public int detailIdx = 0;

    private void Start()
    {
        for (int i = 0; i < transform.childCount-1; i++)
        {
            guideDetail[i] = guideDetailParent.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if(isOnPanel)
        {
            GameManager.Instance.IsPause = true;
            if(Input.GetMouseButtonDown(0))
            {
                FalseGuide();
            }
        }
    }

    public void OnGuide(int idx)
    {
        if(!isOnPanel)
        {
            isOnPanel = true;

            UIManager.Instance.OnCutScene();

            guidePanel.gameObject.SetActive(true);

            isFirstInput = false;

            falseImage.gameObject.SetActive(true);
            falseImage.DOFade(0, 0.5f).SetLoops(-1, LoopType.Yoyo);

            guideDetail[idx].SetActive(true);

            detailIdx = idx;
        }
    }

    void FalseGuide()
    {
        if (UIManager.Instance._stopMenu.gameObject.activeSelf)
            return;

        isOnPanel = false;
        falseImage.gameObject.SetActive(false);
        guideDetail[detailIdx].SetActive(false);

        if (detailIdx == 0)
        {
            OnGuide(1);
            return;
        }

        guidePanel.transform.DOScale(0f, 0.2f).OnComplete(() =>
        {
            guidePanel.SetActive(false);
            UIManager.Instance.OnCutSceneOverWithoutClearDialog();
            
            guidePanel.transform.localScale = new Vector3(1f, 1f, 1f);
        });
    }
}
