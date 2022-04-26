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

    ShowInventoryUI inventoryUI;
    bool isActive = false;

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
                Debug.Log("asd");

                itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
                    GetComponentInChildren<Item>().gameObject.transform.
                    Rotate(0f, -Input.GetAxis("Mouse X") * speed, 0f, Space.World);
                itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
                    GetComponentInChildren<Item>().gameObject.transform.
                    Rotate(-Input.GetAxis("Mouse Y") * speed, 0f, 0f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Inventory.Instance.MainItem == null)
                return;

            

            if(!isActive)
            {
                isActive = !isActive;
                inventoryUI.isOnInventory = true;
                GameManager.Instance.IsPause = true;
                UIManager._instance.DisplayCursor(true);

                itemInfoPanel.SetActive(true);
                itemInfoPanel.transform.DOScale(1f, 0.1f);


                itemImage.texture = itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
                    GetComponent<Camera>().targetTexture;
                itemRole.text = itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
                    GetComponentInChildren<Item>().itemInfo.itemRole;
            }
            else
            {
                isActive = !isActive;
                inventoryUI.isOnInventory = false;
                GameManager.Instance.IsPause = false;
                UIManager._instance.DisplayCursor(false);

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
        itemImage.texture = itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
                    GetComponent<Camera>().targetTexture;
        itemRole.text = itemIconCamParent.transform.GetChild(Inventory.Instance.mainItemIndex).
            GetComponentInChildren<Item>().itemInfo.itemRole;
    }
}
