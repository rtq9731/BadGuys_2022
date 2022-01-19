using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Inventory : MonoBehaviour
{
    static Inventory instance = null;

    public static Inventory Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Inventory>();
            }
            return instance;
        }
    }

    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject slotParents;

    //public Button[] buttons;
    
    //public Slot[] slots;

    //public GameObject itemRolePanel;
    //public Text itemRoleText;
    //public Image itemImage;

    private Slot curItemSlot;

    private InventoryContentsSize contentsSize;
    private CreatSlot creatSlot;

    public ItemInfo MainItem;
    public int mainItemIndex;

    private void Start()
    {
        contentsSize = GetComponentInChildren<InventoryContentsSize>();
        creatSlot = GetComponentInChildren<CreatSlot>();
        //slots = slotParents.GetComponentsInChildren<Slot>();
        //buttons = slotParents.GetComponentsInChildren<Button>();

        //for (int i = 0; i < buttons.Length-1; i++)
        //{
        //    buttons[i].onClick.AddListener(() =>
        //    {
        //        ShowItemInfo();
        //    });
        //}
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!inventoryPanel.activeSelf)
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

        InventoryReset();
    }

    public void PickUpItem(ItemInfo _item)
    {
        for (int i = 0; i < slotParents.transform.childCount; i++)
        {
            if (_item == slotParents.transform.GetChild(i).GetComponent<Slot>().item)
            {
                slotParents.transform.GetChild(i).GetComponent<Slot>().UpdateItemSlot();
                return;
            }
        }
        
        creatSlot.CreatingSlot();
        InventoryContentsSize.Instance.SetContentsSize();

        Debug.Log(slotParents.transform.GetChild(slotParents.transform.childCount - 1));
        slotParents.transform.GetChild(slotParents.transform.childCount - 1).GetComponent<Slot>().AddItem(_item);

        if(slotParents.transform.childCount == 1)
        {
            MainItem = _item;
        }
    }

    public void InventoryReset()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            MainItem = null;
            for (int i = 0; i < slotParents.transform.childCount; i++)
            {
                Destroy(slotParents.transform.GetChild(i).gameObject);
            }
            InventoryContentsSize.Instance.SetContentsSize();
        }
    }

    //public void ShowItemInfo()
    //{
    //    itemRolePanel.SetActive(true);

    //    curItemSlot = EventSystem.current.currentSelectedGameObject.GetComponent<Slot>();

    //    itemRoleText.text = curItemSlot.itemRole;
    //    itemImage.sprite = curItemSlot.itemImage.sprite;
    //}

    
}
