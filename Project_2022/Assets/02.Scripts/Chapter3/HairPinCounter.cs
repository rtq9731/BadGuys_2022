using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HairPinCounter : MonoBehaviour
{
    public Item[] item;
    public ItemInfo itemInfo;
    public GameObject slotParents;

    public DialogDatas touchOneDialog;
    public DialogDatas noTouchDialog;
    public DialogDatas TouchDialog;

    private Slot slot;
    public bool isTouch;

    private void Start()
    {
        isTouch = false;

        if (item.Length < 1)
        {
            Debug.LogError("No Hair Pins");
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (!IsDouble() && (item.Length > 0) && isTouch)
        {
            item[0].dialogs = touchOneDialog.GetDialogs().ToList();
            item[1].dialogs = touchOneDialog.GetDialogs().ToList();
        }

        if (Inventory.Instance.MainItem == itemInfo)
        {
            if (IsDouble() && (item.Length > 0))
            {
                if (!isTouch)
                    item[0].dialogs = noTouchDialog.GetDialogs().ToList();
                else
                    item[0].dialogs = TouchDialog.GetDialogs().ToList();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private bool IsDouble()
    {
        if (Inventory.Instance.MainItem == null)
            return false;

        
        slot = slotParents.transform.GetChild(Inventory.Instance.mainItemIndex).GetComponent<Slot>();
        if (slot.itemCount >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsSingle()
    {
        if (Inventory.Instance.MainItem == null)
            return false;

        slot = slotParents.transform.GetChild(Inventory.Instance.mainItemIndex).GetComponent<Slot>();
        if (slot.itemCount == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
