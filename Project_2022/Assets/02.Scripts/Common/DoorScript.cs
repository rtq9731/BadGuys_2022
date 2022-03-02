using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoorScript : MonoBehaviour, IInteractableItem
{
    [SerializeField] GameObject door;
    [SerializeField] [Range(-180, 180)] float maxRotation;
    [SerializeField] float duration = 2f;

    bool isOpen = false;

    public void Interact(GameObject taker)
    {
        if (!isOpen)
        {
            door.transform.DOLocalRotate(Vector3.forward * maxRotation, duration);
        }
        else
        {
            door.transform.DOLocalRotate(Vector3.zero, duration);
        }
    }
}
