using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopInterect : MonoBehaviour, IInteractableItem
{
    public GameObject laptopUI;

    private bool isUIOn;

    public void Interact(GameObject taker)
    {
        isUIOn = true;
        LaptopManager.Instance.LapTopOn();

    }

    public bool CanInteract()
    {
        return !isUIOn;
    }
}
