using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cabinet : MonoBehaviour, IInteractableItem
{
    public bool _isOpen = false;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact(GameObject taker)
    {
        OpenCabinet(_isOpen);
    }

    void OpenCabinet(bool isOpen)
    {
        if (isOpen)
        {
            this._isOpen = false;
            anim.SetTrigger("IsClose");
        }
        else
        {
            this._isOpen = true;
            anim.SetTrigger("IsOpen");
        }
    }

    public bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;

        return true;
    }
}
