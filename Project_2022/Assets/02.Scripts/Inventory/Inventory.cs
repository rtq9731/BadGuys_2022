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

    //public Button[] buttons;

    //public Slot[] slots;

    //public GameObject itemRolePanel;
    //public Text itemRoleText;
    //public Image itemImage;

    private Slot curItemSlot;

    private InventoryContentsSize contentsSize;
    private CreatSlot creatSlot;
    private CreateRenderTextureCam createRender;
    private ShowInventoryUI showInventory;
    private TutorialEmphasis tutorialEmphasis;

    public ItemInfo MainItem;
    public int mainItemIndex;

    public bool isInteract = true;
    public float interactCooltime = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        contentsSize = GetComponentInChildren<InventoryContentsSize>();
        creatSlot = GetComponentInChildren<CreatSlot>();
        createRender = FindObjectOfType<CreateRenderTextureCam>();
        showInventory = GetComponentInChildren<ShowInventoryUI>();
    }

    private void Update()
    {
        if (!isInteract)
            interactCooltime += Time.deltaTime;
        if (interactCooltime > 0.25f)
        {
            isInteract = true;
            interactCooltime = 0;
        }
    }

    public void PickUpItem(ItemInfo item, GameObject obj, GameObject whoIsTaker)
    {
        if(isInteract)
        {
            isInteract = !isInteract;

            for (int i = 0; i < slotParents.transform.childCount; i++)
            {
                if (item == slotParents.transform.GetChild(i).GetComponent<Slot>().item)
                {
                    StartCoroutine(OverlapItem(obj));
                    slotParents.transform.GetChild(i).GetComponent<Slot>().UpdateItemSlot();
                    showInventory.ShowInventorySlot();
                    return;
                }
            }

            creatSlot.CreatingSlot();
            showInventory.ShowInventorySlot();
            //InventoryContentsSize.Instance.SetContentsSize();

            slotParents.transform.GetChild(slotParents.transform.childCount - 1).GetComponent<Slot>().AddItem(item);
            slotParents.transform.GetChild(slotParents.transform.childCount - 1).GetComponent<Slot>().slotItem = obj;

            if (slotParents.transform.childCount == 1)
            {
                MainItem = item;
            }

            ItemInfoPanelManager.Instance.SetDialog(item);

            UIManager.Instance.StartCoroutine(EatItem(obj));    
        }
    }

    public void InventoryReset()
    {
        MainItem = null;
        for (int i = 0; i < slotParents.transform.childCount; i++)
        {
            Destroy(slotParents.transform.GetChild(i).gameObject);
        }
        //InventoryContentsSize.Instance.SetContentsSize();
    }

    IEnumerator OverlapItem(GameObject obj)
    {
        Collider col = obj.transform.GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        Rigidbody rigid = obj.transform.GetComponent<Rigidbody>();
        if (rigid != null)
            rigid.useGravity = false;

        float objScale = obj.transform.localScale.x;

        obj.transform.DOScale(0, 0.4f);

        float t = 0f;

        while (true)
        {
            t += Time.deltaTime / 1f;
            obj.transform.position = Vector3.Lerp(obj.transform.position, itemEatPos.position, t);

            if (Vector3.Distance(obj.transform.position, itemEatPos.position) < 0.1f)
            {
                obj.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }

    IEnumerator EatItem(GameObject obj)
    {
        Collider col = obj.transform.GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        Rigidbody rigid = obj.transform.GetComponent<Rigidbody>();
        if (rigid != null)
            rigid.useGravity = false;

        float objScale = obj.transform.localScale.x;

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
        DOTween.Kill(obj.transform);
        createRender.CreateRenderCam(obj, objScale);
    }

    //public void ShowItemInfo()
    //{
    //    itemRolePanel.SetActive(true);

    //    curItemSlot = EventSystem.current.currentSelectedGameObject.GetComponent<Slot>();

    //    itemRoleText.text = curItemSlot.itemRole;
    //    itemImage.sprite = curItemSlot.itemImage.sprite;
    //}


    public bool FindItemInInventory(ItemInfo itemInfo)
    {
        for (int i = 0; i < slotParents.transform.childCount; i++)
        {
            if(itemInfo == slotParents.transform.GetChild(i).GetComponent<Slot>().item)
            {
                return true;
            }
        }
        return false;
    }
}
