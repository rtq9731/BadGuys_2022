using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowInventoryUI : MonoBehaviour
{
    public float fadeTime;

    private RectTransform rectTrm;
    private RectTransform originRectTrm;

    private void Start()
    {
        rectTrm = GetComponent<RectTransform>();
        originRectTrm = rectTrm;
        
    }

    private void Update()
    {
        ShowInventorySlot();
        fadeTime += Time.deltaTime;
    }

    public void ShowInventorySlot()
    {
        if (fadeTime >= 4f)
        {
            rectTrm.transform.DOMove(new Vector3(rectTrm.transform.position.x, -100), 1f).SetEase(Ease.Linear);
        }
    }
}
