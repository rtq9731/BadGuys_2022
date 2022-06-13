using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HairPinCounter : MonoBehaviour
{
    public Item[] item;
    public ItemInfo itemInfo;
    public GameObject slotParents;

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
