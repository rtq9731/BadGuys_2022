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

    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        originPos = transform.position;
    }

    public override void Interact(GameObject taker)
    {
        OpenCabinet(isOpen);
    }

    void OpenCabinet(bool _isOpen)
    {
        if (_isOpen)
        {
            isOpen = false;
            anim.SetTrigger("IsClose");
        }
        else
        {
            isOpen = true;
            anim.SetTrigger("IsOpen");
        }
    }
}
