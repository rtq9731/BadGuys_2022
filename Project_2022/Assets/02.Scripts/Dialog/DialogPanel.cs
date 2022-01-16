using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class DialogPanel : MonoBehaviour
{
    [SerializeField] Text text;

    public RectTransform rectTrm = null;
    Rect originRect = Rect.zero;

    event Action _onDisable;

    public int order;

    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
        originRect = rectTrm.rect;
        rectTrm.sizeDelta = Vector2.zero;
    }

    private void Start()
    {
        SetActive(true, () => { }, "¤¾¤·");
    }

    public void SetActive(bool active, Action callBack, string str = "")
    {
        float height = 0f;
        if (active)
        {
            gameObject.SetActive(active);
            DOTween.To(() => height, height => rectTrm.sizeDelta = new Vector2(originRect.width, height), originRect.height, 0.3f).OnComplete(() =>
            {
                text.text = str;
                callBack();
            });
        }
        else
        {
            height = originRect.height;
            text.DOFade(0, 0.3f).OnComplete(() =>
            {
                DOTween.To(() => height, height => rectTrm.sizeDelta = new Vector2(originRect.width, height), 0, 0.3f).OnComplete(() =>
                {
                    gameObject.SetActive(active);
                    callBack();
                });
            });
        }
    }
}
