using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cabinet : Item
{
    Vector3 originPos;

    public float endPoint = 0.45f;

    public bool isOpen = false;

    public float speed;

    private void Start()
    {
        originPos = transform.position;
    }

    public override void Interact()
    {
        Debug.Log("¿ÀÇÂ Ä³ºñ³Ý");
        OpenCabinet(isOpen);
    }

    void OpenCabinet(bool _isOpen)
    {
        if (_isOpen)
        {
            isOpen = false;
            transform.DOMove(originPos, speed);
        }
        else
        {
            isOpen = true;
            transform.DOMoveZ(endPoint, speed);
        }
    }
}
