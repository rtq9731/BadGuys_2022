using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FailedQTEImage : MonoBehaviour
{
    CanvasGroup canvasGroup;

    float time = 0.3f;
    float disappearGauge = 0.3f;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

    }

    private void Update()
    {
        disappearGauge -= Time.unscaledDeltaTime;

        canvasGroup.alpha = disappearGauge / time;

        if(canvasGroup.alpha <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
