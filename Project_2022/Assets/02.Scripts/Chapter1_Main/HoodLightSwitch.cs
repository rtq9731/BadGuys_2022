using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoodLightSwitch : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private GameObject hoodLight;
    [SerializeField]
    private AudioSource hoodSound;

    private void Awake()
    {
        if (hoodLight == null)
            Debug.LogWarning("�ĵ����Ʈ ���� �ȵǾ� ����");
        else
            hoodLight.SetActive(false);
    }

    public void Interact(GameObject taker)
    {
        if (hoodLight.activeSelf)
        {
            hoodLight.SetActive(false);
            //hoodSound.Stop();
        }
        else
        {
            hoodLight.SetActive(true);
            //hoodSound.Play();
        }
    }

    public bool CanInteract()
    {
        return true;
    }
}
