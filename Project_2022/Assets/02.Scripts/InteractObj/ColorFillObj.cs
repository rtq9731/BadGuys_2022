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

        foreach (var item in colorKeyAndObjs)
        {
            item.unFilledObj.GetComponent<SpriteRenderer>().material.SetFloat("_DissolveAmount", 1);
        }
    }

    public virtual void Interact(ItemInfo itemInfo, GameObject taker)
    {
        ColorKeyAndObj obj = colorKeyAndObjs.Find(item => item.keyItem == inventory.MainItem);
        colorKeyAndObjs.Remove(obj);
        if (obj != null)
        {
            obj.outline.enabled = false;
            if (colorKeyAndObjs.Count < 1)
            {
                obj.RemoveUnFilledObj(removeDuration, _onComplete);
                return;
            }
            invenInput.RemoveItem();
            obj.RemoveUnFilledObj(removeDuration, () => { });
        }
    }

    public void OnPlayerMouseEnter()
    {
        _onPlayerMouseEnter?.Invoke();
        ColorKeyAndObj obj = colorKeyAndObjs.Find(item => item.keyItem == inventory.MainItem);
        if (obj != null)
        {
            obj.outline.enabled = true;
        }
    }

    public void OnPlayerMouseExit()
    {
        RemoveOutlines();
    }

    public void RemoveOutlines()
    {
        colorKeyAndObjs.ForEach(item =>
        {
            item.outline.enabled = false;
        });
    }

    public bool CanInteract(ItemInfo itemInfo)
    {
        if (!gameObject.activeSelf || !enabled)
            return false;

        ColorKeyAndObj obj = colorKeyAndObjs.Find(item => item.keyItem == inventory.MainItem);
        return obj != null;
    }

    [System.Serializable]
    public class ColorKeyAndObj
    {
        public ItemInfo keyItem = null;
        public GameObject unFilledObj = null;
        public Outline outline = null;

        public void RemoveUnFilledObj(float duration, System.Action callBack)
        {
            unFilledObj.GetComponent<SpriteRenderer>().material.DOFloat(0, "_DissolveAmount", duration).OnComplete(() =>
            {
                callBack?.Invoke();
            });
        }
    }
}


