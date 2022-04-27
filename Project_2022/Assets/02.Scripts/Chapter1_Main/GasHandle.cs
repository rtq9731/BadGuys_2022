using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GasHandle : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private GameObject fire;
    [SerializeField]
    private AudioSource sound;
    [SerializeField]
    private GameObject anotherHandle;
    [SerializeField]
    private float time = 0.5f;
    [SerializeField]
    private float tatataTime = 0.3f;

    private bool ready;

    private void Awake()
    {
        if (fire == null)
        {
            Debug.Log("가스불이 세팅되어 있지 않습니다.");
        }
        else
        {
            fire.SetActive(false);
        }

        ready = true;
    }

    private void TurnOntheFire()
    {
        fire.SetActive(!fire.activeSelf);
        ready = true;
    }

    public void Interact(GameObject taker)
    {
        if (ready)
        {
            if (fire.activeSelf)
            {
                ready = false;
                Invoke("TurnOntheFire", tatataTime);
                anotherHandle.SetActive(!anotherHandle.activeSelf);
                gameObject.SetActive(!gameObject.activeSelf);
                //sound.Stop();
            }
            else
            {
                ready = false;
                Invoke("TurnOntheFire", tatataTime);
                anotherHandle.SetActive(!anotherHandle.activeSelf);
                gameObject.SetActive(!gameObject.activeSelf);
                //sound.Play();
            }
        }
    }

    public bool CanInteract()
    {
        return true;
    }
}
