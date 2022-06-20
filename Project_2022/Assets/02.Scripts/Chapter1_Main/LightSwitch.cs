using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private GameObject targetLight;

    [SerializeField] AudioClip turnOnSound;
    [SerializeField] AudioClip turnOffSound;

    [SerializeField]
    private AudioSource clickSound;

    private void Awake()
    {
        if (targetLight == null)
            Debug.LogWarning("타겟 라이트 설정 안되어 있다.");
        clickSound = GetComponentInParent<AudioSource>();
        //clickSound.playOnAwake = false;
    }

    public void Interact(GameObject taker)
    {
        Vector3 meTr = gameObject.transform.localEulerAngles;
        targetLight.SetActive(!targetLight.activeSelf);
        //clickSound.Play();
        if (gameObject.transform.rotation.z == 0)
        {
            gameObject.transform.localEulerAngles = new Vector3(meTr.x, meTr.y, 180);
            clickSound.clip = turnOnSound;
            clickSound.Play();
        }
        else
        {
            gameObject.transform.localEulerAngles = new Vector3(meTr.x, meTr.y, 0);
            clickSound.clip = turnOffSound;
            clickSound.Play();
        }
    }

    public bool CanInteract()
    {
        return true;
    }
}
