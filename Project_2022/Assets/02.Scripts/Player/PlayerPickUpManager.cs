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

    float pickUpCoolTime = 0f;

    public void CanPickUpItem(IInteractableItem curItem)
    {
        if(curItem.CanInteract())
        {
            ShowPickUpIcon(true);
            if (pickUpCoolTime >= 0.5f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ShowPickUpIcon(false);
                    curItem.Interact(itemTakePos);
                    return;
                }
            }
        }
    }

    private void Update()
    {
        pickUpCoolTime += Time.deltaTime;
    }

    public void CanInteractObj(IInteractAndGetItemObj curObj, ItemInfo itemInfo)
    {
        if (curObj.CanInteract(itemInfo))
        {
            ShowPickUpIcon(true);

            if(pickUpCoolTime >= 0.5f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickUpCoolTime = 0;
                    curObj.Interact(itemInfo, itemTakePos);
                    ShowPickUpIcon(false);
                    return;
                }
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
