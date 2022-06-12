using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogPanel : MonoBehaviour
{
    RectTransform rectTrm = null;

    Rect originRect = Rect.zero;

    [SerializeField] Text uiText;

    public bool isOn = false;
    public bool isInited = false;

    public int order = 0;
    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
        originRect = rectTrm.rect;
    }

    public void InitPanel()
    {
        uiText.text = "";
        gameObject.SetActive(true);
        isOn = false;
        isInited = true;

        float height = 0f;
        DOTween.To(() => height, height => rectTrm.sizeDelta = new Vector2(originRect.width, height), originRect.height, 0.3f).OnComplete(()=> 
        {
            isOn = true;
        });
    }

    public void SetTextColor(Color color)
    {
        uiText.color = color;
    }

    public void InitPanelNow()
    {
        isOn = false;
        isInited = true;

        rectTrm.sizeDelta = new Vector2(originRect.width, originRect.height);

        isOn = true;
    }

    public bool IsOn()
    {
        return isOn;
    }

    public void SetText(char text)
    {
        uiText.text += text;
    }

    public void Remove()
    {
        float height = rectTrm.rect.height;
        uiText.DOFade(0, 0.3f).OnComplete(() =>
        {
            DOTween.To(() => height, height => rectTrm.sizeDelta = new Vector2(originRect.width, height), 0, 0.3f).OnComplete(() =>
            {
                isOn = false;
                gameObject.SetActive(false);
                isInited = false;
            });
        });
    }

    public void RemoveNow()
    {
        uiText.text = "";
        gameObject.SetActive(false);
        isInited = false;
    }
}
