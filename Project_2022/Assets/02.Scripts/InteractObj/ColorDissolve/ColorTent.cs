using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorTent : ColorRemoveObjParent
{
    bool canInteract = true;

    public override void Interact(ItemInfo itemInfo, GameObject taker)
    {
        if (!canInteract)
            return;

        if (itemInfo.itemName == keyItem.itemName)
        {
            DOTween.To(() => dissolveMat.GetFloat("_NoiseStrength"), (float value) => dissolveMat.SetFloat("_NoiseStrength", value), dissolveStrength, dissolveDuration);
            itemObj.SetActive(true);
            FindObjectOfType<Inventory>().PickUpItem(returnItem, itemObj, taker);
            canInteract = false;
        }
    }

}
