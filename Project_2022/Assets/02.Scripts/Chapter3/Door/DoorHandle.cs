using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DoorHandle : MonoBehaviour, IInteractableItem
{
    public GameObject peekCam;
    public GameObject peekUI;
    public Image fadeImg;

    public void Interact(GameObject taker)
    {
        
    }

    public bool CanInteract()
    {
        return true;
    }
}
