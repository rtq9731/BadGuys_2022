using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProfileScrollView : MonoBehaviour
{
    [SerializeField] PersonInfoList info;

    [SerializeField] RectTransform numberBtnsParent = null;

    [SerializeField] PatientInfoPanel infoPanel = null;
    [SerializeField] Button numberBtnPrefab = null;

    [SerializeField] List<Button> numberBtns = new List<Button>();
    [SerializeField] Button btnSelect = null;

    int curPanelNum = 0;

    private void Awake()
    {
        btnSelect.onClick.AddListener(OnClickSelect);
        for (int i = 0; i < info.patientInfos.Count; i++)
        {
            int y = i;
            Button curBtn = Instantiate(numberBtnPrefab, numberBtnsParent);
            numberBtns.Add(curBtn);
            curBtn.onClick.AddListener(() => OnClickNumberBtn(y));
        }
    }

    private void Start()
    {
        infoPanel.InitPatientInfoPanel(info.patientInfos[0]);
        ResizeBtns(0);
    }

    private void OnClickNumberBtn(int idx)
    {
        infoPanel.FadeInOut(0.3f, () => infoPanel.InitPatientInfoPanel(info.patientInfos[idx]));
        ResizeBtns(idx);
    }

    private void ResizeBtns(int idx)
    {
        for (int i = 0; i < numberBtns.Count; i++)
        {
            if (i != idx)
            {
                numberBtns[i].GetComponent<RectTransform>().DOSizeDelta(new Vector2(24, 24), 0.3f);
            }
            else
            {
                numberBtns[i].GetComponent<RectTransform>().DOSizeDelta(new Vector2(24, 128), 0.3f);
            }
        }
    }

    private void OnClickSelect()
    {
        infoPanel.OnPatientSelect();
        btnSelect.interactable = false;
        for (int i = 0; i < numberBtns.Count; i++)
        {
            numberBtns[i].interactable = false;
        }
    }
}
