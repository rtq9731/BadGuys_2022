using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IronDrawer : MonoBehaviour, IInteractableItem
{
    private bool isOpen;
    private Vector3 myOriPos;

    public float desZ = 0.8f;

    private void Awake()
    {
        isOpen = false;
        myOriPos = transform.localPosition;
        Debug.Log(myOriPos);
    }

    private bool isDoTween()
    {
        return DOTween.IsTweening(transform);
    }

    public void Interact(GameObject taker)
    {
        if (!isDoTween())
        {
            if (isOpen)
            {
                isOpen = false;
                transform.DOLocalMove(myOriPos, 1f);
                Debug.Log(myOriPos);
            }
            else
            {
                isOpen = true;
                Vector3 des = new Vector3(myOriPos.x, myOriPos.y, desZ);
                transform.DOLocalMove(des, 1f);
            }
        }
    }

    public bool CanInteract()
    {
        return true;
    }

}
