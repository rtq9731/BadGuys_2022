using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tent : ColorRemoveObjParent
{
    public override bool CanInteract(ItemInfo itemInfo)
    {
        if (!canInteract || !gameObject.activeSelf || !enabled)
            return false;

        return itemInfo.itemName == keyItem.itemName;
    }

    public override void Interact(ItemInfo itemInfo, GameObject taker)
    {
        if (!canInteract)
            return;

        if (itemInfo.itemName == keyItem.itemName)
        {
            DOTween.To(() => dissolveMat.GetFloat("_NoiseStrength"), (float value) => dissolveMat.SetFloat("_NoiseStrength", value), dissolveStrength, dissolveDuration);
            outline.enabled = false;
            itemObj.SetActive(true);
            inventory.PickUpItem(returnItem, itemObj, taker);
            canInteract = false;
            onInteract?.Invoke();
        }
    }

}
