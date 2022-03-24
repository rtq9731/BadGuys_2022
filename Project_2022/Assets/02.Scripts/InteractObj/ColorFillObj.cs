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

    public event System.Action _onPlayerMouseEnter = null;
    public event System.Action _onComplete = null;

    Inventory inventory = null;
    InventoryInput invenInput = null;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        invenInput = FindObjectOfType<InventoryInput>();
        outline.enabled = false;
    }

    public void Interact(ItemInfo itemInfo, GameObject taker)
    {
        ColorKeyAndObj obj = colorKeyAndObjs.Find(item => item.keyItem == inventory.MainItem);
        colorKeyAndObjs.Remove(obj);
        if (obj != null)
        {
            if(colorKeyAndObjs.Count < 1)
            {
                obj.RemoveUnFilledObj(removeDuration, _onComplete);
            }

            invenInput.RemoveItme();
            obj.RemoveUnFilledObj(removeDuration, () => { });
        }
    }

    public void OnPlayerMouseEnter()
    {
        _onPlayerMouseEnter?.Invoke();
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

    public void RemoveUnFilledObj(float duration, System.Action callBack)
    {
        unFilledObj.GetComponent<SpriteRenderer>().material.DOFloat(0, "_DissolveAmount", duration).OnComplete(() => 
        {
            callBack?.Invoke();
        });
    }
}

