using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorTent : MonoBehaviour, IInteractAndGetItemObj
{
    [SerializeField] ItemInfo keyItem = null;
    [SerializeField] ItemInfo returnItem = null;
    [SerializeField] GameObject itemObj = null;
    [SerializeField] GameObject dissolveObj = null;
    [SerializeField] GameObject originObj = null;

    Material dissolveMat = null;
    bool canInteract = false;

    private void Start()
    {
        dissolveMat = dissolveObj.GetComponent<MeshRenderer>().material;
    }

    public void Interact(ItemInfo itemInfo)
    {
        if (!canInteract)
            return;

        if (itemInfo.itemName == keyItem.itemName)
        {
            DOTween.To(() => dissolveMat.GetFloat("_NoiseStrength"), (float value) => dissolveMat.SetFloat("_NoiseStrength", value), 50f, 3f).OnComplete(() =>
            {
                originObj.SetActive(false);
            });
            FindObjectOfType<Inventory>().PickUpItem(returnItem, null, null);
        }
    }
}
