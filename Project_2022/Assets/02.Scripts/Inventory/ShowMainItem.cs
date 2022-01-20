using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowMainItem : MonoBehaviour
{
    [SerializeField]
    private GameObject slotParent;
    [SerializeField]
    private GameObject mainItemPanel;

    RectTransform rect;

    private float moveSpeed = 0.3f;
    private void Start()
    {
        rect = mainItemPanel.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(slotParent.transform.childCount <= 0)
        {
            mainItemPanel.SetActive(false);
        }
        else
        {
            mainItemPanel.SetActive(true);
        }
    }

    public void MoveMainItemPanel(int _mainItemIndex)
    {
        if (_mainItemIndex > slotParent.transform.childCount - 1)
        {
            return;
        }
        Debug.Log(_mainItemIndex);
        rect.DOMoveX(slotParent.transform.GetChild(_mainItemIndex).position.x, 0.3f).OnComplete(() =>
        {
            Debug.Log(_mainItemIndex);
            transform.SetParent(slotParent.transform.GetChild(_mainItemIndex));
        });
    }

}
