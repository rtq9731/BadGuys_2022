using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuideManager : MonoBehaviour
{

    TutorialEmphasis tutorialEmphasis;
    GuidePanel guidePanel;

    bool isFirstItem = false;
    bool isAllItem = false;

    private void Start()
    {
        tutorialEmphasis = FindObjectOfType<TutorialEmphasis>();
        guidePanel = FindObjectOfType<GuidePanel>();
    }

    private void Update()
    {
        if(IsGetAllCarWithoutRed() && !isAllItem)
        {
            guidePanel.OnGuide(1);
            Debug.Log("������ �����ڵ��� ���� �� �Ծ���");
            isAllItem = true;
        }
    }

    bool IsGetAllCarWithoutRed()
    {
        if (tutorialEmphasis.idx == 6)
            return true;

        return false;
    }
}
