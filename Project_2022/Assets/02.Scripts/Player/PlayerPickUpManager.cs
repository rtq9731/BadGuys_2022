using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUpManager : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private Image mouseImage;
    [SerializeField]
    private Sprite canUseSprite;
    [SerializeField]
    private Sprite baseSprite;


    public void CanPickUpItem(Item curItem)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.PickUpItem(curItem.itemInfo);
            curItem.gameObject.SetActive(false);
            ShowPickUpText(false);
            return;
        }
        ShowPickUpText(true);
    }

    public void ShowPickUpText(bool isItemOn)
    {
        if(isItemOn)
        {
            mouseImage.rectTransform.sizeDelta = new Vector2(64, 64);
            mouseImage.sprite = canUseSprite;
        }
        else
        {
            mouseImage.rectTransform.sizeDelta = new Vector2(16, 16);
            mouseImage.sprite = baseSprite;
        }
    }

    

}
