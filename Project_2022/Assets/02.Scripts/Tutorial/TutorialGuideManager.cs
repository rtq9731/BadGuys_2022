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
            Debug.Log("asd");
            guidePanel.OnGuide(5);
            isAllItem = true;
        }

        if(IsFirstInterect())
        {
            guidePanel.OnGuide(2);
        }
    }

    bool IsFirstInterect()
    {
        if(!isFirstItem)
        {
            if (Inventory.Instance.MainItem != null)
            {
                isFirstItem = true;
                return true;
            }
        }
        return false;
    }

    bool IsGetAllCarWithoutRed()
    {
        if (tutorialEmphasis.idx == 6)
            return true;

        return false;
    }
}
