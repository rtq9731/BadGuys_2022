using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NewDialogPanel : MonoBehaviour
{
    RectTransform rectTrm = null;

    Rect originRect = Rect.zero;

    [SerializeField] Text uiText;

    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
        originRect = rectTrm.rect;
    }

    public void SetText()
    {

    }

    public void Remove()
    {

    }

    public void RemoveNow()
    {
        uiText.text = "";
        gameObject.SetActive(false);
    }
}
