using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpManager : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private GameObject pickUpTxt;

    public void CanPickUpItem(Item curItem)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.PickUpItem(curItem.itemInfo);
            curItem.gameObject.SetActive(false);
        }
        ShowPickUpText(true);
    }

    public void ShowPickUpText(bool isItemOn)
    {
        pickUpTxt.gameObject.SetActive(isItemOn);
    }

    

}
