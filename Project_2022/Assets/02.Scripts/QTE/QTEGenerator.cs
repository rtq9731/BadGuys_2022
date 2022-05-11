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
    [SerializeField] GameObject QTEEventUIShoot;


    [SerializeField] GameObject QTEFailedImage;

    [SerializeField] Image successEffect;
    [SerializeField] Image failedEffect;

    RectTransform uiPosition;

    Image fillImage;
    Text buttonText;

    QTEEvents events;
    QTEShooting shooting;

    float qteTime = 3f;
    float qteGauge = 3f;

    bool isOnQTE = false;

    public GameObject curQTEObj;

    private void Start()
    {
        events = GetComponent<QTEEvents>();
        shooting = FindObjectOfType<QTEShooting>();
    }

    private void Update()
    {
        if (isOnQTE)
        {

            switch(events.QTEKeys[0].pressType)
            {
                case QTEPressType.Single:
                    {
                        qteGauge -= Time.unscaledDeltaTime;
                        fillImage.fillAmount = qteGauge / qteTime;
                        if (fillImage.fillAmount <= 0)
                        {
                            RemoveQTE();
                        }
                    }
                    break;
                case QTEPressType.Roll:
                    {
                        qteGauge -= Time.unscaledDeltaTime;
                        if (qteGauge <= 0)
                        {
                            RemoveQTE();
                        }
                    }
                    break;
                case QTEPressType.Shoot:
                    break;
            }
        }
    }

    public void FailQTE()
    {
        GameObject obj = Instantiate(QTEFailedImage);

        //obj.GetComponent<RectTransform>().anchoredPosition = uiPosition.anchoredPosition;
        buttonText = obj.transform.GetComponentInChildren<Text>();
        uiPosition = obj.GetComponent<RectTransform>();

        buttonText.text = events.QTEKeys[0].QTEKey[0].ToString();
    }

    public void Generation()
    {
        if (events.QTEKeys.Count <= 0)
            return;

        switch (events.QTEKeys[0].pressType)
        {
            case QTEPressType.Single:
                {
                    GameObject qte = Instantiate(QTEEventUISingle);
                    SetQTE(qte, QTEPressType.Single);
                    isOnQTE = true;
                    curQTEObj = qte;
                }
                break;
            case QTEPressType.Roll:
                {
                    GameObject qte = Instantiate(QTEEventUIRoll);
                    SetQTE(qte, QTEPressType.Roll);
                    isOnQTE = true;
                    curQTEObj = qte;
                }
                break;
            case QTEPressType.Shoot:
                {
                    QTEEventUIShoot.SetActive(true);
                    SetQTE(QTEEventUIShoot, QTEPressType.Shoot);
                    isOnQTE = true;
                    curQTEObj = QTEEventUIShoot;
                    shooting.targetObj = curQTEObj;
                }
                break;
        }
    }

    void SetQTE(GameObject qteObj, QTEPressType key)
    {
        qteObj.transform.SetParent(canvas);

        switch (key)
        {
            case QTEPressType.Single:
                {
                    fillImage = qteObj.transform.GetChild(1).GetComponent<Image>();
                    buttonText = qteObj.transform.GetComponentInChildren<Text>();
                    uiPosition = qteObj.GetComponent<RectTransform>();

                    buttonText.text = events.QTEKeys[0].QTEKey[0].ToString();

                    uiPosition.anchoredPosition = new Vector2(0, 0);
                }
                break;
            case QTEPressType.Roll:
                {
                    buttonText = qteObj.transform.GetComponentInChildren<Text>();
                    uiPosition = qteObj.GetComponent<RectTransform>();

                    buttonText.text = events.QTEKeys[0].QTEKey[0].ToString();

                    uiPosition.anchoredPosition = new Vector2(0, 0);
                }
                break;
            case QTEPressType.Shoot:
                {
                    qteObj.transform.DOScale(1f, 0.2f).OnComplete(() =>
                    {
                        qteObj.GetComponent<ShootingTarget>().FadeTarget();
                    });
                }
                break;
        }
    }


    public void FailedQTE()
    {
        failedEffect.DOFade(0.4f, 0f).OnComplete(() =>
        {
            failedEffect.DOFade(0, 0.2f);
        });
    }

    public void SuccessQTE()
    {
        successEffect.DOFade(0.4f, 0f).OnComplete(() =>
        {
            successEffect.DOFade(0, 0.2f);
        });
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
        Destroy(curQTEObj);
    }
}
