using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoodLightSwitch : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private GameObject hoodLight;
    [SerializeField]
    private SoundScript hoodBtnSound;
    [SerializeField]
    private SoundScript hoodWindSound;

    private bool isReady;

    private void Awake()
    {
        if (hoodLight == null)
            Debug.LogWarning("후드라이트 세팅 안되어 있음");
        else
            hoodLight.SetActive(false);

        isReady = true;
    }

    public void Interact(GameObject taker)
    {
        isReady = false;

        if (hoodLight.activeSelf)
        {
            hoodLight.SetActive(false);
            hoodBtnSound.Play();
            hoodWindSound.Pause();
        }
        else
        {
            hoodLight.SetActive(true);
            hoodBtnSound.Play();
            hoodWindSound.Play();
        }

        Invoke("Ready", 0.3f);
    }

    private void Ready()
    {
        isReady = true;
    }

    public bool CanInteract()
    {
        return isReady;
    }
}
