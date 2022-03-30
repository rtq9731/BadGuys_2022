using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorFillObj_prcture : MonoBehaviour, IInteractAndGetItemObj, IPlayerMouseEnterHandler, IPlayerMouseExitHandler
{
    [SerializeField] Outline outline = null;
    [SerializeField] float removeDuration = 2f;

    [SerializeField] List<ItemInfo> keys = new List<ItemInfo>();
    [SerializeField] List<GameObject> fillObjects = new List<GameObject>();
    [SerializeField] GameObject whiteBrushPrefab = null;

    [SerializeField] ItemInfo whiteBrushItemInfo = null;

    public event System.Action _onPlayerMouseEnter = null;
    public event System.Action _onComplete = null;

    Inventory inventory = null;
    InventoryInput invenInput = null;

    int fillCounter = -1;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        invenInput = FindObjectOfType<InventoryInput>();
        outline.enabled = false;
    }

    public void Interact(ItemInfo itemInfo, GameObject taker)
    {
        ItemInfo obj = keys.Find(item => item == itemInfo);
        if (obj != null)
        {
            keys.Remove(obj);
            fillCounter++;
            fillObjects[fillCounter].GetComponent<SpriteRenderer>().material.DOFloat(1, "_DissolveAmount", removeDuration);
            invenInput.RemoveItem();
            inventory.PickUpItem(whiteBrushItemInfo, Instantiate<GameObject>(whiteBrushPrefab), taker);

            if (fillCounter == fillObjects.Count - 1)
            {
                enabled = false;
                _onComplete?.Invoke();
            }
        }
    }

    public void OnPlayerMouseEnter()
    {
        _onPlayerMouseEnter?.Invoke();
        ItemInfo obj = keys.Find(item => item == inventory.MainItem);
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
