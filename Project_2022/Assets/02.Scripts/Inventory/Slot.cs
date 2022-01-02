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

    private int itemCount;

    public void AddItem(ItemInfo _itemInfo)
    {
        item = _itemInfo;
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = _itemInfo.itemImage;
        itemRole = _itemInfo.itemRole;

        isHaveItem = true;
        itemCount = 1;
    }

    public void UpdateItemSlot()
    {
        if(itemCount <= 0)
        {
            isHaveItem = false;

            item = null;
            itemImage.sprite = null;
            itemImage.gameObject.SetActive(false);
        }    
    }
}
