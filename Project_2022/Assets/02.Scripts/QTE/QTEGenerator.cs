using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QTEGenerator : MonoBehaviour
{

    [SerializeField] Transform canvas;

    [SerializeField] GameObject QTEEventUIRoll;
    [SerializeField] GameObject QTEEventUISingle;

    RectTransform uiPosition;

    Image fillImage;
    Text buttonText;

    QTEEvents events;

    float qteTime = 3f;
    float qteGauge = 3f;

    bool isOnQTE = false;

    public GameObject curQTEObj;

    private void Start()
    {
        events = GetComponent<QTEEvents>();
    }

    private void Update()
    {
        if (isOnQTE)
        {
            if (events.QTEKeys[0].pressType == QTEPressType.Single)
            {
                qteGauge -= Time.unscaledDeltaTime;
                fillImage.fillAmount = qteGauge / qteTime;
                if (fillImage.fillAmount <= 0)
                {
                    RemoveQTE();
                }
            }
            else
            {
                qteGauge -= Time.unscaledDeltaTime;
                if (qteGauge <= 0)
                {
                    RemoveQTE();
                }
            }
        }
    }

    public void Generation()
    {
        if (events.QTEKeys.Count <= 0)
            return;

        if (events.QTEKeys[0].pressType == QTEPressType.Single)
        {
            GameObject qte = Instantiate(QTEEventUISingle);
            SetQTE(qte);
            isOnQTE = true;
            curQTEObj = qte;
        }
        else
        {
            GameObject qte = Instantiate(QTEEventUIRoll);
            SetQTE(qte);
            isOnQTE = true;
            curQTEObj = qte;
        }
    }

    void SetQTE(GameObject qteObj)
    {
        qteObj.transform.SetParent(canvas);

        if (events.QTEKeys[0].pressType == QTEPressType.Single)
        {
            fillImage = qteObj.transform.GetChild(1).GetComponent<Image>();
        }

        buttonText = qteObj.transform.GetComponentInChildren<Text>();
        uiPosition = qteObj.GetComponent<RectTransform>();

        buttonText.text = events.QTEKeys[0].QTEKey[0].ToString();
        uiPosition.anchoredPosition = new Vector3(0, 0, 0);
    }

    public void RollBtn()
    {
        curQTEObj.transform.DOScale(1.2f, 0.2f).OnComplete(() =>
        {
            curQTEObj.transform.DOScale(1.0f, 0.1f).SetUpdate(true);
        }).SetUpdate(true);
    }

    public void RemoveQTE()
    {
        isOnQTE = false;
        qteGauge = 2.5f;
        Destroy(curQTEObj, 0.3f);
    }
}
