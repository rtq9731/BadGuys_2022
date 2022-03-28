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
        Debug.Log("맞았음");
        isPressed = true;
        transform.DOLocalMoveY(12.8f, 0.1f);
        orderPuzzle.CheckAnswer(this.gameObject);
    }

    public void Return()
    {
        Debug.Log("틀렸음");
        transform.DOLocalMoveY(12.8581f, 0.1f);
        isPressed = false;
    }
    
}
