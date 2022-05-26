using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneInteract : MonoBehaviour, IInteractableItem
{
    public ItemInfo itemInfo;
    public GameObject phonePre;

    public void Interact(GameObject taker)
    {
           
    }
    
    public bool CanInteract()
    {
        return true;
    }
}
