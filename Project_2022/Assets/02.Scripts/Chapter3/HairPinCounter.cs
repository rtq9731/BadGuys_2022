using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HairPinCounter : MonoBehaviour
{
    public Item[] item;
    public ItemInfo itemInfo;
    public GameObject slotParents;

    public DialogDatas dialog;

    private Slot slot;

    private void Start()
    {
        if (item.Length < 1)
        {
            Debug.LogError("No Hair Pins");
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Inventory.Instance.MainItem == itemInfo)
        {
            if (IsDouble() && (item.Length > 0))
            {
                item[0].dialogs = dialog.GetDialogs().ToList();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private bool IsDouble()
    {
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
}
