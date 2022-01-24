using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractableItem
{
    public ItemInfo itemInfo;

    public virtual void Interact(GameObject taker)
    {
        Inventory.Instance.PickUpItem(itemInfo, this.gameObject, taker);
    }
}
