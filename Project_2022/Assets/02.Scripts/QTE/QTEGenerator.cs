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

    [SerializeField] Sprite[] QTESprites;

    RectTransform uiPosition;

    Image fillImage;
    Image buttonImage;


    QTEEvents events;
    QTEShooting shooting;

    float qteTime = 3f;
    float qteGauge = 3f;

    public bool isOnQTE = false;

    public GameObject curQTEObj;

    int qteUiIndex = 0;

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
        
        uiPosition = obj.GetComponent<RectTransform>();

        
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
                    buttonImage = qteObj.transform.GetChild(2).GetChild(0).GetComponent<Image>();
                    
                    uiPosition = qteObj.GetComponent<RectTransform>();

                    buttonImage.sprite = QTESprites[qteUiIndex];
                    qteUiIndex++;

                    uiPosition.anchoredPosition = new Vector2(0, 0);
                }
                break;
            case QTEPressType.Roll:
                {
                    Animation anim = qteObj.transform.GetChild(0).GetComponent<Animation>();
                    anim.Play();

                    fillImage = qteObj.transform.GetChild(2).GetComponent<Image>();
                    buttonImage = qteObj.transform.GetChild(3).GetChild(0).GetComponent<Image>();

                    uiPosition = qteObj.GetComponent<RectTransform>();

                    buttonImage.sprite = QTESprites[qteUiIndex];
                    qteUiIndex++;

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
