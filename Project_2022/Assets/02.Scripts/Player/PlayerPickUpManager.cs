using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUpManager : MonoBehaviour
{
    [SerializeField]
    private GameObject itemTakePos;
    [SerializeField]
    private Image interactImage = null;

    bool isShowedInteractImage = false;

    public void CanPickUpItem(IInteractableItem curItem)
    {
        ShowPickUpIcon(true);
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowPickUpIcon(false);
            curItem.Interact(itemTakePos);
            return;
        }
    }

    public void CanInteractObj(IInteractAndGetItemObj curObj, ItemInfo itemInfo)
    {
        if (curObj.CanInteract(itemInfo))
        {
            ShowPickUpIcon(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("EEEEEEEEEEEEEEEEEEE");
                curObj.Interact(itemInfo, itemTakePos);
                ShowPickUpIcon(false);
                return;
            }
        }
    }

    public void ShowPickUpIcon(bool isItemOn)
    {
        if(isShowedInteractImage == isItemOn)
        {
            return;
        }

        isShowedInteractImage = isItemOn;
        interactImage.gameObject.SetActive(isItemOn);
    }



}
