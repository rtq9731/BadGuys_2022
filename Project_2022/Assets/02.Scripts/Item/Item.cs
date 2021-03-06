using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractableItem
{
    public List<DialogData> dialogs = null;
    public ItemInfo itemInfo;
    public event Action onInteract = () => { };

    public virtual void Interact(GameObject taker)
    {
        onInteract();
        Inventory.Instance.PickUpItem(itemInfo, this.gameObject, taker);
        if(dialogs != null && dialogs.Count > 0)
        {
            DialogManager.Instance.SetDialogData(dialogs);
        }
    }

    public virtual bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;

        return true;
    }
}
