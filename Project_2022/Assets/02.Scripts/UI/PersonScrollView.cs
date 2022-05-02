using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PersonScrollView : MonoBehaviour
{
    [SerializeField] List<PatientInfoPanel> panels = new List<PatientInfoPanel>();
    [SerializeField] PatientInfoPanel panelPrefab = null;

    [SerializeField] Button btnLeft, btnRight, btnSelect;

    int curPanelNum = 0;

    private void Awake()
    {
        btnLeft.onClick.AddListener(OnClickLeft);
        btnRight.onClick.AddListener(OnClickRight);
        btnSelect.onClick.AddListener(OnClickSelect);
    }

    private void OnEnable()
    {
        panels.ForEach(item => item.transform.DOComplete());
        // RefreshPanels();
    }

    public void MakePatientInfoPanel(PersonInfoList data)
    {
        panels.ForEach(item => Destroy(item.gameObject));
        panels.Clear();

        foreach (var item in data.patientInfos)
        {
            panels.Add(GetNewPanel(item));
        }

        RefreshPanels();
    }

    private PatientInfoPanel GetNewPanel(PersonInfo data)
    {
        PatientInfoPanel result = null;
        
        result = Instantiate<PatientInfoPanel>(panelPrefab, transform);
        result.InitPatientInfoPanel(data);

        return result;
    }

    private void OnClickLeft()
    {
        panels.ForEach(item => item.transform.DOComplete());
        
        if (curPanelNum == 0)
            return;

        curPanelNum--;
        
        RefreshPanels();
    }

    private void OnClickRight()
    {
        panels.ForEach(item => item.transform.DOComplete());
        
        if (curPanelNum == panels.Count - 1)
            return;

        curPanelNum++;

        RefreshPanels();
    }

    private void OnClickSelect()
    {
        panels[curPanelNum].OnPatientSelect();

        btnLeft.interactable = false;
        btnRight.interactable = false;
        btnSelect.interactable = false;
    }

    private void RefreshPanels()
    {
        btnSelect.interactable = panels[curPanelNum].canStart;
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

            panels[i].GetComponent<RectTransform>().DOAnchorPos(new Vector3(375 * posNumber, 64 * absPosNumber - 1, 0), 0.5f);

            if (i == curPanelNum)
            {
                panels[i].transform.SetAsLastSibling();
            }
        }
    }
}
