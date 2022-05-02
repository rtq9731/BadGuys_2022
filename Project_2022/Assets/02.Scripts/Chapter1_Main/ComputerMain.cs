using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMain : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private GameObject screenObj;
    [SerializeField]
    private AudioSource bootingSound;

    private void Awake()
    {
        if (screenObj == null)
            Debug.LogWarning("스크린 캔버스 세팅 안되어있음");
        else
            screenObj.SetActive(false);
    }

    public void Interact(GameObject taker)
    {
        screenObj.SetActive(!screenObj.activeSelf);
    }

    public bool CanInteract()
    {
        return true;
    }
}
