using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaflet : MonoBehaviour, IInteractableItem
{
    public void Interact(GameObject taker)
    {
        GetComponent<SoundScript>().Play();
        GetComponent<Collider>().enabled = false;
        Invoke("Pop", 0.7f);
    }

    public bool CanInteract()
    {
        return true;
    }

    private void Pop()
    {
        gameObject.SetActive(false);
    }
}
