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
            if (instance == null)
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
    [SerializeField]
    private Transform itemEatPos;
    [SerializeField]
    private Transform itemViewPos;


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
    }

    public void PickUpItem(ItemInfo _item, GameObject obj, GameObject whoIsTaker)
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
        slotParents.transform.GetChild(slotParents.transform.childCount - 1).GetComponent<Slot>().slotItem = obj;

        if (slotParents.transform.childCount == 1)
        {
            MainItem = _item;
        }

        StartCoroutine(EatItem(obj));
    }

    public void InventoryReset()
    {
        MainItem = null;
        for (int i = 0; i < slotParents.transform.childCount; i++)
        {
            Destroy(slotParents.transform.GetChild(i).gameObject);
        }
        InventoryContentsSize.Instance.SetContentsSize();
    }

    IEnumerator EatItem(GameObject obj)
    {
        obj.transform.GetComponent<Collider>().enabled = false;
        obj.transform.GetComponent<Rigidbody>().useGravity = false;
        obj.transform.SetParent(itemViewPos);
        obj.transform.DOScale(0, 0.4f);

        float t = 0f;

        while (true)
        {
            t += Time.deltaTime / 1f;
            obj.transform.position = Vector3.Lerp(obj.transform.position, itemEatPos.position, t);

            if(Vector3.Distance(obj.transform.position, itemEatPos.position) <0.1f )
            {
                obj.gameObject.SetActive(false);
                break;
            }
            yield return null;
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
