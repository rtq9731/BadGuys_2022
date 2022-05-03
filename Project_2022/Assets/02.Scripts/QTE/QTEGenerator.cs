using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QTEGenerator : MonoBehaviour
{

    [SerializeField] Transform canvas;
    [SerializeField] GameObject QTEEventUI;
    
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
        if(isOnQTE)
        {
            qteGauge -= Time.deltaTime;
            fillImage.fillAmount = qteGauge / qteTime;
            if (fillImage.fillAmount <= 0)
            {
                RemoveQTE();
            }
        }
    }

    public void Generation()
    {
        if (events.QTEKeys.Count <= 0)
            return;

        GameObject qte = Instantiate(QTEEventUI);
        SetQTE(qte);
        isOnQTE = true;
        curQTEObj = qte;
    }

    void SetQTE(GameObject qteObj)
    {
        qteObj.transform.SetParent(canvas);

        buttonText = qteObj.transform.GetComponentInChildren<Text>();
        uiPosition = qteObj.GetComponent<RectTransform>();
        fillImage = qteObj.transform.GetChild(1).GetComponent<Image>();

        buttonText.text = events.QTEKeys[0].QTEKey[0].ToString();
    }

    public void RemoveQTE()
    {
        curQTEObj.transform.DOScale(1.2f, 0.2f).OnComplete(() =>
        {
            curQTEObj.transform.DOScale(1.0f, 0.1f).OnComplete(() =>
            {
                CheckAnswer();
                Destroy(curQTEObj);
                qteGauge = 2.5f;
                isOnQTE = false;
            });
        });
    }

    void CheckAnswer()
    {
        
    }
}
