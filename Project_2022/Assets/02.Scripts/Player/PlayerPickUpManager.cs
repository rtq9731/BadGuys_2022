using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpManager : MonoBehaviour
{
    [SerializeField] 
    private float _playerArmLength = 1f;
    [SerializeField]
    private LayerMask _whatIsTouchable;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private GameObject pickUpTxt;

    private GameObject curItem;
    private bool isOnMouse;
    


    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _playerArmLength, _whatIsTouchable))
        {
            if (hitInfo.point != null)
            {
                curItem?.GetComponent<IPlayerMouseEnterHandler>()?.OnPlayerMouseEnter();
                curItem = hitInfo.transform.gameObject;

                CanPickUpItem(curItem.GetComponent<Item>());

                Debug.Log(curItem.transform.name);
                isOnMouse = true;
            }
        }
        else
        {
            curItem?.GetComponent<IPlayerMouseExitHandler>()?.OnPlayerMouseExit();
            isOnMouse = false;
        }

        ShowPickUpText();
    }

    private void CanPickUpItem(Item curItem)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.PickUpItem(curItem.itemInfo);
            curItem.gameObject.SetActive(false);
        }
    }

    public void ShowPickUpText()
    {
        if (isOnMouse)
        {
            pickUpTxt.gameObject.SetActive(true);
        }
        else 
        {
            pickUpTxt.gameObject.SetActive(false);
        }
    }

    

}
