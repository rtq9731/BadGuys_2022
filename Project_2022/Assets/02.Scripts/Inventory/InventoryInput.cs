using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    Dictionary<KeyCode, int> keyDic;

    private int itemIndex = 0;

    public ShowMainItem showMainItem;

    bool isCanRemove;

    [SerializeField]
    Transform slotsParent;
    void Start()
    {
        keyDic = new Dictionary<KeyCode, int>()
        {
             {KeyCode.Alpha1, 1},
             {KeyCode.Alpha2, 2},
             {KeyCode.Alpha3, 3},
             {KeyCode.Alpha4, 4},
             {KeyCode.Alpha5, 5},
             {KeyCode.Alpha6, 6},
             {KeyCode.Alpha7, 7},
             {KeyCode.Alpha8, 8},
             {KeyCode.Alpha9, 9}
        };
    }

    private void Update()
    {
        TryInputNumber();
        RemoveItme();
    }

    

    private void TryInputNumber()
    {
        if(slotsParent.childCount > 0)
        {
            if (Input.anyKeyDown)
            {
                foreach (var dic in keyDic)
                {
                    if (Input.GetKeyDown(dic.Key))
                    {
                        ChangeSlot(dic.Value - 1);
                        itemIndex = dic.Value - 1;

                        showMainItem.MoveMainItemPanel(itemIndex);
                    }
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                itemIndex--;
                itemIndex = Mathf.Clamp(itemIndex, 0, slotsParent.childCount - 1);
                ChangeSlot(itemIndex);
                showMainItem.MoveMainItemPanel(itemIndex);
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                itemIndex++;
                itemIndex = Mathf.Clamp(itemIndex, 0, slotsParent.childCount - 1);
                ChangeSlot(itemIndex);
                showMainItem.MoveMainItemPanel(itemIndex);
            }

            
        }
    }

    void ChangeSlot(int slotNum)
    {
        if(slotNum > slotsParent.childCount-1)
        {
            return;
        }
        Inventory.Instance.MainItem = slotsParent.GetChild(slotNum).GetComponent<Slot>().item;
        Inventory.Instance.mainItemIndex = slotNum;
    }

    public void RemoveItme()
    {
        if(Input.GetKeyDown(KeyCode.E) && isCanRemove)
        {
            if (slotsParent.childCount > 0)
            {
                GameObject destroySlot = slotsParent.GetChild(Inventory.Instance.mainItemIndex).gameObject;
                destroySlot.transform.parent = null;
                Destroy(destroySlot);

                InventoryContentsSize.Instance.SetContentsSize();

                if(slotsParent.childCount > 0)
                {
                    if(Inventory.Instance.mainItemIndex == 0)
                    {
                        SetMainItem(itemIndex);
                    }
                    else
                    {
                        Inventory.Instance.mainItemIndex--;
                        SetMainItem(itemIndex);
                    }
                }
            }
        }
    }
    
    void SetMainItem(int _mainItemIndex)
    {
        Inventory.Instance.MainItem = slotsParent.GetChild(_mainItemIndex).GetComponent<Slot>().item;
        showMainItem.MoveMainItemPanel(_mainItemIndex-1);
    }


    public void RemoveItmeFalse(IInteractableItem curItem)
    {
        isCanRemove = false;
    }
    public void CanRemoveItme(bool _isCanRemove)
    {
        isCanRemove = _isCanRemove;
    }
}
