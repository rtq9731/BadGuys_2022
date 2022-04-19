using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Paint : Item, IPlayerMouseEnterHandler, IPlayerMouseExitHandler
{
    [SerializeField] Outline outline = null;

    [SerializeField] List<ItemInfo> keys = new List<ItemInfo>();
    [SerializeField] GameObject returnItemPrfab = null;

    [SerializeField] Color paintColor = Color.white;
    [SerializeField] MeshRenderer paintMs = null;

    public event System.Action _onPlayerMouseEnter = null;
    public event System.Action _onComplete = null;

    Inventory inventory = null;
    InventoryInput inveninput = null;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inveninput = FindObjectOfType<InventoryInput>();

        paintMs.material.color = paintColor;
        outline.enabled = false;
    }

    public override void Interact(GameObject taker)
    {
        ItemInfo item = keys.Find(item => inventory.MainItem == item);
        if (item != null)
        {
            inveninput.RemoveItem();
            Inventory.Instance.PickUpItem(itemInfo, Instantiate(returnItemPrfab, transform.position, Quaternion.identity), taker);
        }
    }

    public override bool CanInteract()
    {
        if(!enabled || !gameObject.activeSelf)
        {
            return false;
        }

        return keys.Find(item => inventory.MainItem == item);
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
