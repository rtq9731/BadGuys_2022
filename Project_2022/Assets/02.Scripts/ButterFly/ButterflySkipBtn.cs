using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButterflySkipBtn : MonoBehaviour
{
    ButterFlyScript butterfly = null;

    [SerializeField] Image gaugeImage = null;

    RectTransform myRect = null;

    Vector2 originPos = Vector2.zero;

    bool isOff = false;

    float disappearGauge = 0f;
    float disappearMax = 2f;

    float skipgaugeTime = 3f;
    float skipgauge = 0;

    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        originPos = myRect.anchoredPosition;
    }
    private void Start()
    {
        butterfly = FindObjectOfType<ButterFlyScript>();
    }

    public void SetActive(bool isActive)
    {
        transform.parent.gameObject.SetActive(isActive);
        if(isActive)
        {
            gaugeImage.fillAmount = 0f;
            isOff = false;
            skipgauge = 0f;
            myRect.anchoredPosition = originPos;
        }
    }

    private void Update()
    {
        disappearGauge += Time.deltaTime;
        if (Input.GetKey(KeyCode.E))
        {
            if(isOff)
            {
                myRect.DOAnchorPosY(originPos.y, 0.5f);
                isOff = false;
            }

            skipgauge += Time.deltaTime;
            gaugeImage.fillAmount = skipgauge / skipgaugeTime;
            disappearGauge = 0f;
            if (skipgauge >= skipgaugeTime)
            {
                Skip();
            }
        }

        if (disappearGauge >= disappearMax && !isOff)
        {
            gaugeImage.fillAmount = skipgauge / skipgaugeTime;
            myRect.DOAnchorPosY(-128, 0.5f);
            isOff = true;
        }
    }

    private void Skip()
    {
        gaugeImage.fillAmount = 0f;
        skipgauge = 0f;
        gaugeImage.fillAmount = skipgauge / skipgaugeTime;
        butterfly.SkipButterFlyMove();
    }
}
