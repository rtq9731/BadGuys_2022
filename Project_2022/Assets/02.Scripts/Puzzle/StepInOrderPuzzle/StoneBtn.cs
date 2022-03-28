using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StoneBtn : MonoBehaviour
{
    public OrderPuzzle orderPuzzle;

    public bool isPressed;

    private void Start()
    {
        orderPuzzle = transform.parent.GetComponent<OrderPuzzle>();
    }

    public void Pressed()
    {
        Debug.Log("�¾���");
        isPressed = true;
        transform.DOLocalMoveY(12.8f, 0.1f);
        orderPuzzle.CheckAnswer(this.gameObject);
    }

    public void Return()
    {
        Debug.Log("Ʋ����");
        transform.DOLocalMoveY(12.8581f, 0.1f);
        isPressed = false;
    }
    
}
