using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public bool isInventoryActived = false;

    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject slotParents;

    public Button[] buttons;
    
    public Slot[] slots;

    public GameObject itemRolePanel;
    public Text itemRoleText;
    public Image itemImage;

    private Slot curItemSlot;

    private void Start()
    {
        slots = slotParents.GetComponentsInChildren<Slot>();
        buttons = slotParents.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length-1; i++)
        {
            buttons[i].onClick.AddListener(() =>
            {
                ShowItemInfo();
            });
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
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

    public void ShowItemInfo()
    {
        itemRolePanel.SetActive(true);

        curItemSlot = EventSystem.current.currentSelectedGameObject.GetComponent<Slot>();

        itemRoleText.text = curItemSlot.itemRole;
        itemImage.sprite = curItemSlot.itemImage.sprite;
    }
}
