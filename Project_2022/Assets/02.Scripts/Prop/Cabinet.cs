using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cabinet : MonoBehaviour, IInteractableItem
{
    public bool _isOpen = false;

    Animator anim;

    AudioSource audioSource;
    
    [SerializeField] AudioClip[] clip;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponentInParent<AudioSource>();
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
            audioSource.clip = clip[1];
        }
        else
        {
            this._isOpen = true;
            anim.SetTrigger("IsOpen");
            audioSource.clip = clip[0];
        }
        audioSource.Play();
    }

    public bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;

        return true;
    }
}
