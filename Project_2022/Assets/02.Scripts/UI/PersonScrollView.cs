using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PersonScrollView : MonoBehaviour
{
    [SerializeField] List<PatientInfoPanel> panels = new List<PatientInfoPanel>();
    [SerializeField] PatientInfoPanel panelPrefab = null;
    [SerializeField] RectTransform numberBtnPrefab = null;

    [SerializeField] List<Button> numberBtns = new List<Button>();
    [SerializeField] Button btnSelect = null;

    int curPanelNum = 0;

    private void Awake()
    {
        btnSelect.onClick.AddListener(OnClickSelect);
    }

    public void MakePatientInfoPanel(PersonInfoList data)
    {
        panels.ForEach(item => Destroy(item.gameObject));
        panels.Clear();

        curPanelNum = 0;
        foreach (var item in data.patientInfos)
        {
            panels.Add(GetNewPanel(item));
        }

        RefreshPanels();
    }

    private PatientInfoPanel GetNewPanel(PersonInfo data)
    {
        PatientInfoPanel result = Instantiate<PatientInfoPanel>(panelPrefab, transform);
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

        btnSelect.interactable = false;
    }

    private void RefreshPanels()
    {
        btnSelect.interactable = panels[curPanelNum].canStart;
        int minNum = 0;
        int maxNum = 0;

        for (int i = 0; i < numberBtns.Count; i++)
        {

        }

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

            panels[i].GetComponent<RectTransform>().DOAnchorPos(new Vector3(175 * posNumber, 600 * posNumber, 0), 0.5f);

            if (i == curPanelNum)
            {
                panels[i].transform.SetAsLastSibling();
            }
        }
    }
}
