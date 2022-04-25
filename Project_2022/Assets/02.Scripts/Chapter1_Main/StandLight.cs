using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandLight : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private GameObject targetLight;
    [SerializeField]
    private AudioSource clickSound;

    private void Awake()
    {
        if (targetLight == null)
            Debug.LogWarning("타겟 라이트 설정 안되어 있다.");
        else
            targetLight.SetActive(false);
        //clickSound = GetComponent<AudioSource>();
        //clickSound.playOnAwake = false;
    }

    public void Interact(GameObject taker)
    {
        targetLight.SetActive(!targetLight.activeSelf);
        //clickSound.Play();
    }

    public bool CanInteract()
    {
        return true;
    }
}
