using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bridge : ColorRemoveObjParent
{
    [SerializeField] List<GameObject> dissolveObjs = new List<GameObject>();
    List<Material> dissolveMats = new List<Material>();

    public override void Start()
    {
        onInteract += () => enabled = false;
        for (int i = 0; i < dissolveObjs.Count; i++)
        {
            Material curMat = dissolveObjs[i].GetComponent<MeshRenderer>().material;
            curMat.SetFloat("_NoiseScale", noiseScale);
            dissolveMats.Add(curMat);
        }
        inventory = FindObjectOfType<Inventory>();
        outline.enabled = false;
    }

    public override void Interact(ItemInfo itemInfo, GameObject taker)
    {
        if (!canInteract)
            return;


        if (itemInfo.itemName == keyItem.itemName)
        {
            for (int i = 0; i < dissolveMats.Count; i++)
            {
                Material dissolveMat = dissolveMats[i];
                Debug.Log(dissolveMat);
                DOTween.To(() => dissolveMat.GetFloat("_NoiseStrength"), (float value) => dissolveMat.SetFloat("_NoiseStrength", value), dissolveStrength, dissolveDuration);
            }

            itemObj.SetActive(true);
            inventory.PickUpItem(returnItem, itemObj, taker);
            canInteract = false;
            onInteract?.Invoke();
        }
    }

}
