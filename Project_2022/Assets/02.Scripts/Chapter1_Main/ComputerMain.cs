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
            Debug.LogWarning("��ũ�� ĵ���� ���� �ȵǾ�����");
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
