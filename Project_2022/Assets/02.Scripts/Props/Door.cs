using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour , IInteractableItem
{
    public bool isOpen = false;

    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact(GameObject taker)
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
