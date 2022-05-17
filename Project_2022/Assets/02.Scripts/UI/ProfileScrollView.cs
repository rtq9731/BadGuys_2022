using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ProfileScrollView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] PersonInfoList info;

    [SerializeField] RectTransform numberBtnsParent = null;

    [SerializeField] PatientInfoPanel infoPanel = null;
    [SerializeField] Button numberBtnPrefab = null;

    [SerializeField] List<Button> numberBtns = new List<Button>();
    [SerializeField] Button btnSelect = null;

    Vector2 dragStartPos = Vector2.zero;
    Vector2 dragEndPos = Vector2.zero;

    int curPanelNum = 0;

    float inputCool = 0.5f;
    float lastInputCool = 0f;

    bool isTipNeed = false;

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
        isTipNeed = true;
    }

    private void Update()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        
        if(lastInputCool + inputCool <= Time.time)
        {
            int lastPanelNum = curPanelNum;
            if (wheelInput > 0)
            {
                curPanelNum--;
                curPanelNum = Mathf.Clamp(curPanelNum, 0, numberBtns.Count - 1);

                if (lastPanelNum == curPanelNum)
                    return;

                OnClickNumberBtn(curPanelNum);
            }
            else if (wheelInput < 0)
            {
                curPanelNum++;
                curPanelNum = Mathf.Clamp(curPanelNum, 0, numberBtns.Count - 1);

                if (lastPanelNum == curPanelNum)
                    return;

                OnClickNumberBtn(curPanelNum);
            }
        }
    }

    private void OnClickNumberBtn(int idx)
    {
        int lastPanelNum = curPanelNum;
        curPanelNum = idx;

        if (lastPanelNum == curPanelNum)
            return;

        infoPanel.FadeInOut(0.3f, () => infoPanel.InitPatientInfoPanel(info.patientInfos[idx]));

        ResizeBtns(idx);
    }

    private void ResizeBtns(int idx)
    {
        if(isTipNeed)
        {
            Destroy(transform.parent.GetComponent<UIInteractHelpPanelCaller>());
            isTipNeed = false;
        }

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

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStartPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragEndPos = eventData.position;

        int lastPanelNum = curPanelNum;

        if (dragStartPos.y - dragEndPos.y > 0)
        {
            curPanelNum--;
            curPanelNum = Mathf.Clamp(curPanelNum, 0, numberBtns.Count - 1);

            if (lastPanelNum == curPanelNum)
                return;

            OnClickNumberBtn(curPanelNum);
        }
        else if (dragStartPos.y - dragEndPos.y < 0)
        {
            curPanelNum++;
            curPanelNum = Mathf.Clamp(curPanelNum, 0, numberBtns.Count - 1);

            if (lastPanelNum == curPanelNum)
                return;

            OnClickNumberBtn(curPanelNum);
        }
    }
}
