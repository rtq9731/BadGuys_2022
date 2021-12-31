using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool isInventoryActived = false;

    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private GameObject slotParents;

    private Slot[] slots;

    private void Start()
    {
        slots = slotParents.GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            isInventoryActived = !isInventoryActived;

            if(isInventoryActived)
            {
                inventoryPanel.SetActive(true);
                GameManager._instance._isPaused = true;
                UIManager._instance.DisplayCursor(isInventoryActived);
            }
            else
            {
                inventoryPanel.SetActive(false);
                GameManager._instance._isPaused = false;
                UIManager._instance.DisplayCursor(isInventoryActived);
            }    
        }
    }

    public void PickUpItem(ItemInfo _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item);
                return;
            }
        }
    }
}
