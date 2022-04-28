using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaflet : MonoBehaviour, IInteractableItem
{
    public void Interact(GameObject taker)
    {
        gameObject.SetActive(false);
    }

    public bool CanInteract()
    {
        return true;
    }
}
