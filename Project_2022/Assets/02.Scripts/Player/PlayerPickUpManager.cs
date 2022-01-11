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


    public void CanPickUpItem(IInteractableItem curItem)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowPickUpIcon(false);
            curItem.Interact();
            return;
        }
        ShowPickUpIcon(true);
    }

    public void ShowPickUpIcon(bool isItemOn)
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
