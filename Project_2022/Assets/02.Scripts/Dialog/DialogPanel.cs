using System.Linq;
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

    List<Tween> tweens = new List<Tween>();

    Coroutine cor = null;

    public int order;

    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
        originRect = rectTrm.rect;
        rectTrm.sizeDelta = Vector2.zero;
    }

    public void SetActive(bool active, Color color = default, string str = "", Action callBack = null)
    {
        float height = 0f;
        if (active)
        {
            if(cor != null)
            {
                StopCoroutine(cor);

                if(tweens.Count > 1)
                {
                    
                    foreach (var item in tweens)
                    {
                        item.Complete();
                    }
                }
            }

            text.text = "";
            text.color = color;
            gameObject.SetActive(active);
            cor = StartCoroutine(RemovePanel(5f));

            tweens.Add(DOTween.To(() => height, height => rectTrm.sizeDelta = new Vector2(originRect.width, height), originRect.height, 0.3f).OnComplete(() =>
            {
                Debug.Log(str);
                text.text = str;
                callBack?.Invoke();
            }));
        }
        else
        {
            height = originRect.height;
            tweens.Add(text.DOFade(0, 0.3f).OnComplete(() =>
            {
                tweens.Add(DOTween.To(() => height, height => rectTrm.sizeDelta = new Vector2(originRect.width, height), 0, 0.3f).OnComplete(() =>
                {
                    gameObject.SetActive(active);
                    callBack?.Invoke();
                }));
            }));
        }
    }

    public void SetActiveFalseImmediately()
    {
        if(cor != null)
        {
            StopCoroutine(cor);
        }

        foreach (var item in tweens)
        {
            item.Kill();
        }

        SetActive(false);
    }

    IEnumerator RemovePanel(float time)
    {
        yield return new WaitForSeconds(time);
        SetActive(false);
    }
}
