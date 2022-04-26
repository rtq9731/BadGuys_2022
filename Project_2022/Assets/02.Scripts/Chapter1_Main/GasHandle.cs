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
    private Vector3 oriRot;
    [SerializeField]
    private float tatataTime = 0.3f;

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

        oriRot = transform.localEulerAngles;
    }

    private void TurnOntheFire()
    {
        fire.SetActive(!fire.activeSelf);
    }

    public void Interact(GameObject taker)
    {
        if (fire.activeSelf)
        {
            Invoke("TurnOntheFire", tatataTime);
            //transform.DOLocalRotate(oriRot, time);
            //sound.Stop();
        }
        else
        {
            Invoke("TurnOntheFire", tatataTime);
            //transform.DOLocalRotate(new Vector3(130, oriRot.y, oriRot.z), time);
            //sound.Play();
        }
    }

    public bool CanInteract()
    {
        return true;
    }
}
