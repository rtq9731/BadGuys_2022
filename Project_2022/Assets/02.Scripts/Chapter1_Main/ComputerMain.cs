using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMain : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private GameObject screenCanvas;
    [SerializeField]
    private AudioSource bootingSound;

    private void Awake()
    {
        if (screenCanvas == null)
            Debug.LogWarning("스크린 캔버스 세팅 안되어있음");
        else
            screenCanvas.SetActive(false);
    }

    public void Interact(GameObject taker)
    {
        screenCanvas.SetActive(!screenCanvas.activeSelf);
    }

    public bool CanInteract()
    {
        return true;
    }
}
