using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUpManager : MonoBehaviour
{
    [SerializeField]
    private GameObject itemTakePos;

    public void CanPickUpItem(IInteractableItem curItem)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            curItem.Interact(itemTakePos);
            return;
        }
    }

    public void CanInteractObj(IInteractAndGetItemObj curObj, ItemInfo itemInfo)
    {
        Debug.Log(curObj);
        if (Input.GetKeyDown(KeyCode.E))
        {
            curObj.Interact(itemInfo, itemTakePos);
            return;
        }
    }

    public void ShowPickUpIcon(bool isItemOn)
    {
        //if(isItemOn)
        //{
        //    mouseImage.rectTransform.sizeDelta = new Vector2(64, 64);
        //    mouseImage.sprite = canUseSprite;
        //}
        //else
        //{
        //    mouseImage.rectTransform.sizeDelta = new Vector2(16, 16);
        //    mouseImage.sprite = baseSprite;
        //}
    }

    

}
