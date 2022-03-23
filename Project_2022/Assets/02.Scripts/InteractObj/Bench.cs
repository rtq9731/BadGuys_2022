using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour, IInteractableItem
{
    public event System.Action onInteract = null;

    public void Interact(GameObject taker)
    {
        onInteract?.Invoke();
    }
}
