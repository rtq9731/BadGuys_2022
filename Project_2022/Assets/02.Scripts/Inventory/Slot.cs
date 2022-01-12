using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private bool isHaveItem;

    public ItemInfo item;
    public Image itemImage;
    public string itemRole;

    public Text itemCountText;
    private int itemCount;


    public void AddItem(ItemInfo _itemInfo)
    {
        item = _itemInfo;
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = _itemInfo.itemImage;
        itemRole = _itemInfo.itemRole;

        isHaveItem = true;
        itemCountText.text = null;
        itemCount = 1;
    }

    public void UpdateItemSlot()
    {
        itemCount++;

        if(itemCount <= 1)
        {
            itemCountText = null;
        }
        else
        {
            itemCountText.text = $"{itemCount}X";
        }
    }

    public void DestroyItemSlot()
    {
        if(itemCount <= 0)
        {
            InventoryContentsSize.Instance.SetContentsSize();
            Destroy(this.gameObject);
        }    
    }

    private void Update()
    {
        DestroyItemSlot();
    }
}
