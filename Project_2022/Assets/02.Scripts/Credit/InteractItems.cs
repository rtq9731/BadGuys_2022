using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItems : MonoBehaviour, IInteractableItem
{

    public event System.Action OnPlayerMouseEnter = null;

    public bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;

        return true;
    }

    public void Interact(GameObject taker)
    {
        OnPlayerMouseEnter?.Invoke();
    }
}
