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

    private int mainItemIndex;

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
            MoveMainItemPanel(Inventory.Instance.mainItemIndex);
        }
    }

    public void MoveMainItemPanel(int _mainItemIndex)
    {
        rect.DOMoveX(slotParent.transform.GetChild(_mainItemIndex).position.x, 0.3f);

        //rect.position = slotParent.transform.GetChild(_mainItemIndex).GetComponent<RectTransform>().position;
    }

}
