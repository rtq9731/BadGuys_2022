using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemInfoPanel : MonoBehaviour
{
    public Text uiText = null;
    public int order;

    public RectTransform rect = null;

    Coroutine cor = null;

    Image image = null;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        Debug.Log(rect.sizeDelta);
    }

    private void OnEnable()
    {
        rect.sizeDelta = new Vector2(256, 100);
        StartCoroutine(RemoveCorutine());
    }

    public void RemovePanel()
    {
        image.DOFade(0, 0.5f).OnComplete(() => gameObject.SetActive(false));
        if (cor != null)
            StopCoroutine(cor);
    }

    public void SetText(string text)
    {
        if (this.uiText == null)
            this.uiText = GetComponentInChildren<Text>();

        if (this.image == null)
            this.image = GetComponent<Image>();

        this.uiText.text = text;
        
        GetComponent<Image>().color = Color.white;
    }

    IEnumerator RemoveCorutine()
    {
        yield return new WaitForSeconds(3f);
        image.DOFade(0, 0.5f).OnComplete(() => gameObject.SetActive(false));
        DOTween.To(() => rect.sizeDelta, x => rect.sizeDelta = x, Vector2.zero, 0.5f);
    }

}
