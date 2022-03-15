using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bridge : ColorRemoveObjParent
{
    [SerializeField] List<GameObject> dessolveObjs = new List<GameObject>();
    List<Material> dessolveMats = new List<Material>();

    public override void Start()
    {
        base.Start();
        for (int i = 0; i < dessolveObjs.Count; i++)
        {
            dessolveMats.Add(dessolveObjs[i].GetComponent<MeshRenderer>().material);
        }
    }

    public override void Interact(ItemInfo itemInfo, GameObject taker)
    {
        if (!canInteract)
            return;


        if (itemInfo.itemName == keyItem.itemName)
        {
            foreach (var item in dessolveMats)
            {
                Material dessolveMat = item;
                DOTween.To(() => dissolveMat.GetFloat("_NoiseStrength"), (float value) => dissolveMat.SetFloat("_NoiseStrength", value), dissolveStrength, dissolveDuration);
            }

            itemObj.SetActive(true);
            inventory.PickUpItem(returnItem, itemObj, taker);
            canInteract = false;
        }
    }

}
