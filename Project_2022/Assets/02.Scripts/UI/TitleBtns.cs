using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleBtns : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] RectTransform panelInfo = null;
    RectTransform myRect = null;
    [SerializeField] string infoString = "";

    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject(0))
        {
            panelInfo.gameObject.SetActive(true);
            panelInfo.GetComponentInChildren<Text>().text = infoString;
            panelInfo.anchoredPosition = myRect.anchoredPosition + Vector2.up * 115;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject(0))
        {
            panelInfo.gameObject.SetActive(false);
        }
    }
}