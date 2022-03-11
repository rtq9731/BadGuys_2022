using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorTent : MonoBehaviour, IInteractAndGetItemObj
{
    [SerializeField] ItemInfo keyItem = null;
    [SerializeField] ItemInfo returnItem = null;
    [SerializeField] OutlinerOnMouseEnter outline = null;
    [SerializeField] GameObject itemObj = null;
    [SerializeField] GameObject dissolveObj = null;
    [SerializeField] GameObject originObj = null;

    [SerializeField] float duration = 0f;

    Material dissolveMat = null;
    bool canInteract = true;

    private void Start()
    {
        dissolveMat = dissolveObj.GetComponent<MeshRenderer>().material;
    }

    public void Interact(ItemInfo itemInfo, GameObject taker)
    {
        if (!canInteract)
            return;

        if (itemInfo.itemName == keyItem.itemName)
        {
            DOTween.To(() => dissolveMat.GetFloat("_NoiseStrength"), (float value) => dissolveMat.SetFloat("_NoiseStrength", value), 50f, duration);
            itemObj.SetActive(true);
            outline.gameObject.SetActive(false);
            FindObjectOfType<Inventory>().PickUpItem(returnItem, itemObj, taker);
            canInteract = false;
        }
    }
}
