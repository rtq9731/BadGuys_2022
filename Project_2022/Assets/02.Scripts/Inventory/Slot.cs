using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private bool isHaveItem;

    public ItemInfo item;
    public RawImage itemImage;
    public string itemRole;

    public Text itemCountText;
    public int itemCount;

    public Text itemNum;

    public GameObject slotItem;

    public void AddItem(ItemInfo _itemInfo)
    {
        item = _itemInfo;
        //itemImage.gameObject.SetActive(true);
        //itemImage = _itemInfo.itemImage;
        itemRole = _itemInfo.itemRole;

        initSlotInfo();
    }


    void initSlotInfo()
    {
        isHaveItem = true;
        itemCountText.text = null;
        itemCount = 1;

        if(transform.parent.childCount > 9)
        {
            itemNum.text = null;
        }
        else
        {
            itemNum.text = transform.parent.childCount.ToString();

        }
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

    public void DecreaseItemCount()
    {
        itemCount--;

        if (itemCount <= 1)
        {
            itemCountText.text = null;
            Debug.Log("하나바께 없어요");
        }
        else
        {
            itemCountText.text = $"{itemCount}X";
            Debug.Log("여러개 있어요");
        }
    }

    public void DestroyItemSlot()
    {
        if(itemCount <= 0)
        {
            //InventoryContentsSize.Instance.SetContentsSize();
            Destroy(this.gameObject);
        }    
    }

    private void Update()
    {
        DestroyItemSlot();
    }
}
