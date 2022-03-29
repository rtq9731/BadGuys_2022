using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Paint : Item
{
    [SerializeField] List<ItemInfo> keys = new List<ItemInfo>();
    [SerializeField] GameObject returnItemPrfab = null;

    [SerializeField] Color paintColor = Color.white;
    [SerializeField] MeshRenderer paintMs = null;

    Inventory inventory = null;
    InventoryInput inveninput = null;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inveninput = FindObjectOfType<InventoryInput>();

        paintMs.material.color = paintColor;
    }

    public override void Interact(GameObject taker)
    {
        ItemInfo item = keys.Find(item => inventory.MainItem == item);
        if (item != null)
        {
            inveninput.RemoveItem();
            Inventory.Instance.PickUpItem(itemInfo, Instantiate(returnItemPrfab), taker);
        }
    }
}
