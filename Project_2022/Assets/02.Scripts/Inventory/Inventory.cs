using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
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

        inventoryPanel.GetComponent<StackableUI>()._onDisable += () =>
        {
            GameManager._instance._isPaused = false;
            UIManager._instance.DisplayCursor(false);
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(!inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(true);
                GameManager._instance._isPaused = true;
                UIManager._instance.DisplayCursor(inventoryPanel.activeSelf);
            }
            else
            {
                UIStackManager.RemoveUIOnTop();
                GameManager._instance._isPaused = false;
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
