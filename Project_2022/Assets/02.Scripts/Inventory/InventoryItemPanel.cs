using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class InventoryItemPanel : MonoBehaviour
{
    [SerializeField] GameObject itemInfoPanel;
    [SerializeField] GameObject itemIconCamParent;
    [SerializeField] RawImage itemImage;
    [SerializeField] Text itemRole;
    [SerializeField] GameObject pickUpPanel;

    ShowInventoryUI inventoryUI;
    bool isActive = false;

    GameObject currentObj = null;
    Vector3 originRotate;

    float speed = 5f;

    private void Start()
    {
        inventoryUI = FindObjectOfType<ShowInventoryUI>();
    }

    void Update()
    {

        if (inventoryUI.isOnInventory)
        {
            if (Input.GetMouseButton(0))
            {
                itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
                    GetComponentInChildren<Item>().gameObject.transform.
                    Rotate(0f, -Input.GetAxis("Mouse X") * speed, 0f, Space.World);
                itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
                    GetComponentInChildren<Item>().gameObject.transform.
                    Rotate(Input.GetAxis("Mouse Y") * speed, 0f, 0f);
            }
        }

        if(isActive)
        {
            if(UIManager._instance.isEsc)
            {
                isActive = !isActive;
                inventoryUI.isOnInventory = false;

                currentObj.transform.rotation = Quaternion.Euler(originRotate);

                itemInfoPanel.transform.DOScale(0f, 0.1f).OnComplete(() =>
                {
                    itemImage.texture = null;
                    itemInfoPanel.SetActive(false);
                });
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Inventory.Instance.MainItem == null)
                return;

            if (UIManager._instance.isEsc)
                return;

            if(!isActive)
            {
                isActive = !isActive;
                inventoryUI.isOnInventory = true;
                GameManager.Instance.IsPause = true;
                UIManager._instance.DisplayCursor(true);

                itemInfoPanel.SetActive(true);
                pickUpPanel.SetActive(false);

                itemInfoPanel.transform.DOScale(1f, 0.1f);


                itemImage.texture = itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
                    GetComponent<Camera>().targetTexture;
                itemRole.text = itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
                    GetComponentInChildren<Item>().itemInfo.itemRole;
                currentObj = itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
                    GetComponentInChildren<Item>().gameObject;


                originRotate = currentObj.transform.rotation.eulerAngles;
            }
            else
            {
                isActive = !isActive;
                inventoryUI.isOnInventory = false;
                GameManager.Instance.IsPause = false;
                UIManager._instance.DisplayCursor(false);

                currentObj.transform.rotation = Quaternion.Euler(originRotate);

                itemInfoPanel.transform.DOScale(0f, 0.1f).OnComplete(() =>
                {
                    itemImage.texture = null;
                    itemInfoPanel.SetActive(false);
                });
            }
        }
    }

    public void UpdateItemIcon()
    {
        inventoryUI.isOnInventory = false;

        currentObj.transform.rotation = Quaternion.Euler(originRotate);
        itemImage.texture = itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
                    GetComponent<Camera>().targetTexture;
        itemRole.text = itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
            GetComponentInChildren<Item>().itemInfo.itemRole;
        currentObj = itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
            GetComponentInChildren<Item>().gameObject;

        inventoryUI.isOnInventory = true;
    }
}
