using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PersonScrollView : MonoBehaviour
{
    [SerializeField] List<RectTransform> panels = new List<RectTransform>();

    [SerializeField] Button btnLeft, btnRight, btnSelect;

    int curPanelNum = 0;

    private void Start()
    {
        panels.ForEach(item => item.transform.DOComplete());
        RefreshPanels();

        btnLeft.onClick.AddListener(OnClickLeft);
        btnRight.onClick.AddListener(OnClickRight);
    }

    private void OnClickLeft()
    {
        panels.ForEach(item => item.transform.DOComplete());
        
        if (curPanelNum == 0)
            return;

        curPanelNum--;
        
        RefreshPanels();
        Debug.Log(curPanelNum);
    }

    private void OnClickRight()
    {
        panels.ForEach(item => item.transform.DOComplete());
        
        if (curPanelNum == panels.Count - 1)
            return;

        curPanelNum++;

        RefreshPanels();
        Debug.Log(curPanelNum);
    }

    private void RefreshPanels()
    {

        int minNum = 0;
        int maxNum = 0;
        for (int i = 0; i < panels.Count; i++)
        {
            int posNumber = i - curPanelNum;

            if(posNumber < minNum)
            {
                minNum = posNumber;
                panels[i].transform.SetAsFirstSibling();
            }
            else if (posNumber > maxNum)
            {
                maxNum = posNumber;
                panels[i].transform.SetAsFirstSibling();
            }

            int absPosNumber = Mathf.Abs(posNumber); // Y 위치를 대칭으로 맞추려면 절댓값이 필요함

            panels[i].DOAnchorPos(new Vector3(375 * posNumber, 64 * absPosNumber - 1, 0), 0.5f);

            if (i == curPanelNum)
            {
                panels[i].SetAsLastSibling();
            }
        }
    }
}
