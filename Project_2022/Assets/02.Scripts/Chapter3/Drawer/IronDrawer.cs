using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IronDrawer : MonoBehaviour, IInteractableItem
{
    private bool isOpen;
    private Vector3 myOriPos;

    public float desZ = 0.8f;

    SoundScript drawSound;

    private void Awake()
    {
        isOpen = false;
        myOriPos = transform.localPosition;
        drawSound = GetComponent<SoundScript>();
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
            }
            else
            {
                isOpen = true;
                Vector3 des = new Vector3(myOriPos.x, myOriPos.y, desZ);
                transform.DOLocalMove(des, 1f);
            }
            drawSound.Play();
        }
        
    }

    public bool CanInteract()
    {
        return true;
    }

}
