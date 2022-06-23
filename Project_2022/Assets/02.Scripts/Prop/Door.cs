using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractableItem
{
    public bool isOpen = false;

    [SerializeField] SoundScript openDoorSound;
    [SerializeField] SoundScript closeDoorSound;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact(GameObject taker)
    {
        OpenDoor(isOpen);
    }

    public bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;

        return true;
    }

    void OpenDoor(bool _isOpen)
    {
        if (_isOpen)
        {
            isOpen = false;
            anim.SetTrigger("IsClose");
            closeDoorSound.Play();
        }
        else
        {
            isOpen = true;
            anim.SetTrigger("IsOpen");
            openDoorSound.Play();
        }
    }
}
