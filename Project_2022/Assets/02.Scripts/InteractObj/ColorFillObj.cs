using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorFillObj : MonoBehaviour, IInteractAndGetItemObj, IPlayerMouseEnterHandler, IPlayerMouseExitHandler
{
    [SerializeField] List<ColorKeyAndObj> colorKeyAndObjs = new List<ColorKeyAndObj>();

    [SerializeField] Outline outline = null;
    [SerializeField] float removeDuration = 2f;

    Inventory inventory = null;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        outline.enabled = false;
    }

    public void Interact(ItemInfo itemInfo, GameObject taker)
    {
        ColorKeyAndObj obj = colorKeyAndObjs.Find(item => item.keyItem == inventory.MainItem);
        if(obj != null)
        {
            obj.RemoveUnFilledObj(removeDuration);
        }
    }

    public void OnPlayerMouseEnter()
    {
        ColorKeyAndObj obj = colorKeyAndObjs.Find(item => item.keyItem == inventory.MainItem);
        if (obj != null)
        {
            outline.enabled = true;
        }
    }

    public void OnPlayerMouseExit()
    {
        outline.enabled = false;
    }
}

[System.Serializable]
public class ColorKeyAndObj
{
    public ItemInfo keyItem = null;
    public GameObject unFilledObj = null;

    public void RemoveUnFilledObj(float duration)
    {
        unFilledObj.GetComponent<SpriteRenderer>().material.DOFloat(0, "_DissolveAmount", duration);
    }
}
