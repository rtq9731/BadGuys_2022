using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandLight : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private GameObject targetLight;
    [SerializeField]
    private SoundScript sound;
    [SerializeField]
    private AudioClip[] clickSound;

    private bool isReady;

    private void Awake()
    {
        if (targetLight == null)
            Debug.LogWarning("타겟 라이트 설정 안되어 있다.");
        else
            targetLight.SetActive(false);

        isReady = true;
    }

    public void Interact(GameObject taker)
    {
        isReady = false;

        if (targetLight.activeSelf) // off
            sound.audioSource.clip = clickSound[0];
        else // on
            sound.audioSource.clip = clickSound[1];
        
        sound.Play();
        Invoke("LightOnOff", 0.75f);
    }

    private void LightOnOff()
    {
        isReady = true;
        targetLight.SetActive(!targetLight.activeSelf);
    }

    public bool CanInteract()
    {
        return isReady;
    }
}
