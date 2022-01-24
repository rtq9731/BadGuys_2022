using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractableItem
{
    public ItemInfo itemInfo;
    public event Action onInteract = () => { };

    public virtual void Interact(GameObject taker)
    {
        onInteract();
        Inventory.Instance.PickUpItem(itemInfo, this.gameObject, taker);
    }
}
