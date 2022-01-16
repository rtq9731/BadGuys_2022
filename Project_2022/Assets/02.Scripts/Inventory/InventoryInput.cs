using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    Dictionary<KeyCode, int> keyDic;

    private int itemIndex = 0;
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
    }

    private void TryInputNumber()
    {
        

        if (Input.anyKeyDown)
        {
            foreach (var dic in keyDic)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    ChangeSlot(dic.Value - 1);
                    itemIndex = dic.Value - 1;
                }
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            itemIndex++;
            itemIndex = Mathf.Clamp(itemIndex, 0, transform.GetChild(0).childCount - 1);
            ChangeSlot(itemIndex);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            itemIndex--;
            itemIndex = Mathf.Clamp(itemIndex, 0, transform.GetChild(0).childCount - 1);
            ChangeSlot(itemIndex);
        }

        Debug.Log(itemIndex);
    }

    void ChangeSlot(int slotNum)
    {
        Inventory.Instance.MainItem = transform.GetChild(0).GetChild(slotNum).GetComponent<Slot>().item;
    }
}
