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
            Invoke("CloseSound", 0.8f);
        }
        else
        {
            isOpen = true;
            anim.SetTrigger("IsOpen");
            Invoke("OpenSound", 0.8f);
        }
    }


    void OpenSound()
    {
        openDoorSound.Play();
    }

    void CloseSound()
    {
        closeDoorSound.Play();
    }
}
