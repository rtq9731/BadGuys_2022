using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class QTEGenerator : MonoBehaviour
{

    [SerializeField] Transform canvas;

    [SerializeField] GameObject QTEEventUIRoll;
    [SerializeField] GameObject QTEEventUISingle;
    [SerializeField] GameObject QTEEventUIShoot;


    [SerializeField] GameObject QTEFailedImage;

    [SerializeField] Image successEffect;
    [SerializeField] Image failedEffect;

    [SerializeField] Sprite wSprite;
    [SerializeField] Sprite eSprite;
    [SerializeField] Sprite aSprite;
    [SerializeField] Sprite dSprite;

    RectTransform uiPosition;

    Image fillImage;
    Image buttonImage;

    QTEEvents events;

    float qteTime = 3f;
    float qteGauge = 3f;

    public bool isOnQTE = false;

    public GameObject curQTEObj;

    private void Awake()
    {
        events = GetComponent<QTEEvents>();
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
            }
        }
    }

    public void FailQTE()
    {
        GameObject obj = Instantiate(QTEFailedImage);

        //obj.GetComponent<RectTransform>().anchoredPosition = uiPosition.anchoredPosition;
        
        uiPosition = obj.GetComponent<RectTransform>();

        
    }

    public void Generation(QTEPressType qTEPressType, KeyCode key)
    {
        switch (qTEPressType)
        {
            case QTEPressType.Single:
                {
                    GameObject qte = Instantiate(QTEEventUISingle);
                    SetQTE(qte, QTEPressType.Single, key);
                    isOnQTE = true;
                    curQTEObj = qte;
                }
                break;
            case QTEPressType.Roll:
                {
                    GameObject qte = Instantiate(QTEEventUIRoll);
                    SetQTE(qte, QTEPressType.Roll, key);
                    isOnQTE = true;
                    curQTEObj = qte;
                }
                break;
            
        }
    }
    
    

    void SetQTE(GameObject qteObj, QTEPressType keyType, KeyCode key)
    {
        qteObj.transform.SetParent(canvas);

        switch (keyType)
        {
            case QTEPressType.Single:
                {

                    fillImage = qteObj.transform.GetChild(1).GetComponent<Image>();
                    buttonImage = qteObj.transform.GetChild(2).GetChild(0).GetComponent<Image>();

                    events.QTEKeys[0].QTEKey[0] = key;
                    events.QTEKeys[0].pressType = keyType;

                    uiPosition = qteObj.GetComponent<RectTransform>();

                    uiPosition.anchoredPosition = new Vector2(0, 0);
                }
                break;
            case QTEPressType.Roll:
                {
                    Animation anim = qteObj.transform.GetChild(0).GetComponent<Animation>();
                    anim.Play();

                    fillImage = qteObj.transform.GetChild(2).GetComponent<Image>();
                    buttonImage = qteObj.transform.GetChild(3).GetChild(0).GetComponent<Image>();

                    events.QTEKeys[0].QTEKey.Add(key);
                    events.QTEKeys[0].pressType = keyType;

                    uiPosition = qteObj.GetComponent<RectTransform>();

                    uiPosition.anchoredPosition = new Vector2(0, 0);
                }
                break;
            
        }
        SetSprite(key);
    }

    void SetSprite(KeyCode key)
    {
        switch(key)
        {
            case KeyCode.W:
                buttonImage.sprite = wSprite;
                break;
            case KeyCode.E:
                buttonImage.sprite = eSprite;
                break;
            case KeyCode.A:
                buttonImage.sprite = aSprite;
                break;
            case KeyCode.D:
                buttonImage.sprite = dSprite;
                break;
            default:
                Debug.LogWarning("지정 되지 않은 키입니다!");
                break;
        }
    }
    public void RollImage(int rollCount, int curCount)
    {
        fillImage.fillAmount = curCount / rollCount;
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
        Debug.Log("asd");
        Destroy(curQTEObj);
    }
}
